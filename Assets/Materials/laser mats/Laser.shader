Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LaserCol ("Colour", Color) = (1,1,1,1)
        _Power ("Power", float) = 1
        _AnimSpeed ("Animation Speed", Range(-5,5)) = 1
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

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal: NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _LaserCol;
            float4 _MainTex_ST;
            float _Power, _AnimSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                // float fresnel = max(0,dot(i.normal, i.viewDir));
                // fresnel = pow(fresnel, 3) * 1;
                // return float4(1,0,0,fresnel);
                
                
                fixed4 noiseTex = tex2D(_MainTex, i.uv - float2(0, _Time.y * _AnimSpeed));
                float4 col = float4(_LaserCol.xyz * noiseTex.xxx, _LaserCol.a/255);
                return col*_Power;
            }
            ENDCG
        }
    }
}
