Shader "Pelle/WaterToonShader"
{
    Properties
    {
        _Color("WaterColor", Color) = (1,1,1,1)
        _Amplitude("Wave Height", Float) = 1
        _WaveLength("Wave Length", Float) = 10
        _WaveSpeed("Wave Speed", Float) = 3
        _DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
        _DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
        _DepthMaxDistance("Depth Maximum Distance", Float) = 1
        _FoamDistance("Foam Distance", Float) = 0.4
        _SurfaceNoiseCutoff("Surface Noise Cutoff", Range(0, 1)) = 0.777
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
            #include "UnityCG.cginc"

            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
                float4 screenPosition : TEXCOORD2;
            };

            float4 _Color;
            float _Amplitude;
            float _WaveLength;
            float _WaveSpeed;
            float4 _DepthGradientShallow;
            float4 _DepthGradientDeep;
            float _FoamDistance;
            float _SurfaceNoiseCutoff;

            float _DepthMaxDistance;

            sampler2D _CameraDepthTexture;

            float4 TurnToWave(float4 v) {
                //Calculate Wave
                float4 wpos = mul(unity_ObjectToWorld, v);
                float k = 2 * UNITY_PI / _WaveLength;
                wpos.y = _Amplitude * sin(k * (wpos.z - _WaveSpeed * _Time.y));
                return mul(unity_WorldToObject, wpos);
            }

            v2f vert (appdata v)
            {
                v2f o;
                float4 vo = v.vertex;

                // Start Recalculating Normals
                float4 tangent = vo + v.tangent;
                float4 bitangent = vo;
                bitangent.xyz = vo.xyz + cross(v.normal, v.tangent);
                   // Calculate Wave Witf "TurnToWave" Function
                      v.vertex = TurnToWave(v.vertex);
                      tangent = TurnToWave(tangent);
                      bitangent = TurnToWave(bitangent);
                v.normal.xyz = normalize(cross(tangent - vo, bitangent - vo));
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                // End Recalculating Normals
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
                float existingDepthLinear = LinearEyeDepth(existingDepth01);
                float depthDifference = existingDepthLinear - i.screenPosition.w;
                float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
                float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);

                float foamDepthDifference01 = saturate(depthDifference / _FoamDistance);
                float foam = _FoamDistance > depthDifference && depthDifference > 0 ? 0.7 -depthDifference : 0;

                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                return (waterColor *  (waterColor*0.5 +NdotL*2)) + foam;
            }
            ENDCG
        }
    }
}
