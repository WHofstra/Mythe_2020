Shader "Unlit/SmootherShading"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color (Lighted)", Color) = (1,1,1,1)
            [HDR]
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32
            [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
    }
        SubShader
        {

            Pass
            {
                Tags
                {
                   "LightMode" = "ForwardBase"
                   "PassFlags" = "OnlyDirectional"
                }
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdbase
                #include "UnityCG.cginc"
                #include "Lighting.cginc"
                #include "AutoLight.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 pos : SV_POSITION;
                    float3 worldNormal : NORMAL;
                    float3 viewDir : TEXCOORD1;
                    SHADOW_COORDS(2)
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _Color;
                float4 _ColorUnlighted;
                float _Glossiness;
                float4 _SpecularColor;
                float4 _RimColor;
                float _RimAmount;

                v2f vert(appdata v)
                {
                    v2f o;

                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    o.viewDir = WorldSpaceViewDir(v.vertex);

                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    TRANSFER_SHADOW(o)
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float3 normal = normalize(i.worldNormal);
                    float3 viewDir = normalize(i.viewDir);
                    float NdotL = dot(_WorldSpaceLightPos0, normal);
                    float shadow = SHADOW_ATTENUATION(i);
                    float lightIntensity = NdotL;

                    float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                    float NdotH = dot(normal, halfVector);

                    float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                    float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                    float4 specular = specularIntensitySmooth * _SpecularColor;

                    float4 rimDot = 1 - dot(viewDir, normal);
                    float rimIntensity = rimDot * NdotL;
                    rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                    float4 rim = rimIntensity * _RimColor;
                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);
                    // apply fog
                    return _Color * col * (0.8*col + lightIntensity * shadow);
                }
                ENDCG
            }
                UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
        }
}
