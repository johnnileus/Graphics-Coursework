Shader "Unlit/Snowfall Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _COLOUR ("Texture Colour", Color) = (1,1,1,1)
        _CUTOFFOUTER ("Camera Fade Outer", Float) = 20
        _CUTOFFINNER ("Camera Fade Inner", Float) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 100

        Pass
        {
            
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float2 depth : TEXCOORD2;
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD4;
                
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _COLOUR;
            float _CUTOFFOUTER;
            float _CUTOFFINNER;
            sampler2D _CameraDepthTexture;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                //return float4(1,0,0,1);
                float depthDist = LinearEyeDepth( tex2D(_CameraDepthTexture, i.screenPos.xy / i.screenPos.w));
                float2 screenSpaceUV = i.screenPos.xy / i.screenPos.w;
                float depth = LinearEyeDepth( SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenSpaceUV));
                
                //distance to fragment
                float dist = distance(i.worldPos, _WorldSpaceCameraPos);
                
                //return float4(1,1,1,1-saturate(depth - dist));
                
                fixed4 col = tex2D(_MainTex, i.uv);
                col = col * _COLOUR;
                
                if (dist > _CUTOFFOUTER)
                {
                    return col;
                }if (dist > _CUTOFFINNER)
                {
                    col.w *= ((dist - _CUTOFFINNER) / (_CUTOFFOUTER - _CUTOFFINNER));
                    return col;
                }
                
                return float4(1,1,1,0);
            }
            ENDCG
        }
    }
}
