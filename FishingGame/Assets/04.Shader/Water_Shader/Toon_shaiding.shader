Shader "Hyun/Toon_shading"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        [HDR]
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32
        [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0,1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0,1)) = 0.1
        _OutlineWidth("OutlineWidth", Float) = 0.01
    }
    SubShader
    {
		Tags 
		{
			"RenderPipeline"="UniversalPipeline"
			"RenderType"="Opaque"
			"Queue"="Geometry+0"
		}
        Pass
		{

			HLSLPROGRAM

        	#pragma vertex vert
        	#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

 			// GPU Instancing
        	#pragma multi_compile_instancing
        	#pragma multi_compile_fog

        	// Receiving Shadow Options
        	#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
        	#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
        	#pragma multi_compile _ _SHADOWS_SOFT

			CBUFFER_START(UnityPerMaterial)
			TEXTURE2D(_MainTex);
        	SAMPLER(sampler_MainTex);
			half4 _MainTex_ST;
			float4 _Color;

			float4 _AmbientColor;

			float4 _SpecularColor;
			float _Glossiness;		

			float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;
			CBUFFER_END

        	struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;

				UNTIY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;	
				float4 shadowCoord : TEXCOORD2;	
				UNTIY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				
			};

            float Dot(float3 normal, float3 direction){
                return normal.x * direction.x +  normal.y * direction.y +normal.z * direction.z;
            }
			
			v2f vert (appdata v)
			{
				v2f o;

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v,o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.vertex = TransformObjectToHClip(v.vertex);
				o.normal = TransformObjectToWorldNormal(v.normal);		
				o.viewDir = WorldSpaceViewDir(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				TRANSFER_SHADOW(o)
				return o;
			}
			
				

			float4 frag (v2f i) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

				float3 normal = normalize(i.normal);
				float3 viewDir = normalize(i.viewDir);

				float NdotL = Dot(_WorldSpaceLightPos0, normal);

				float shadow = SHADOW_ATTENUATION(i);  
				float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);	
				float4 light = lightIntensity * _LightColor0;

				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = Dot(normal, halfVector);
				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;				

				float rimDot = 1 - Dot(viewDir, normal);
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float4 rim = rimIntensity * _RimColor;

				float4 sample = SAMPLE_TEXTURE2D(_MainTex_ST,_MainTex, i.uv);

				return (light + _AmbientColor + specular + rim) * _Color * sample;
			}
        
        ENDHLSL
    }

	cull front
    CGPROGRAM
	
    #pragma surface surf Lambert vertex:vert
	float _OutlineWidth;

    struct Input
    {
            float _Blank;
    };

    void vert(inout appdata_full v){
        v.vertex.xyz += v.normal.xyz*_OutlineWidth;
    }

    void surf (Input IN, inout SurfaceOutput o)
    {
        o.Albedo = 0;
    }
    ENDCG
}
}
