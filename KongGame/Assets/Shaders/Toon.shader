Shader "Unlit/Toon"
{
	//Skeleton taken from https://roystan.net/articles/toon-shader.html tutorial
		Properties
		{
			_Color("Color", Color) = (1, 1, 1, 1)
				_MainTex("Main Texture", 2D) = "white" {}
		// Ambient light is applied uniformly to all surfaces on the object.
		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4, 0.4, 0.4, 1)
			[HDR]
		_SpecularColor("Specular Color", Color) = (0.9, 0.9, 0.9, 1)
			// Controls the size of the specular reflection.
			_Glossiness("Glossiness", Float) = 32
			[HDR]
		_RimColor("Rim Color", Color) = (1, 1, 1, 1)
			_RimAmount("Rim Amount", Range(0, 1)) = 0.716
			// Control how smoothly the rim blends when approaching unlit
			// parts of the surface.
			_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
		}
			SubShader
		{
			Pass
			{

				Tags
				{
				//In pass we pass through the tags
				//Giving data on lighting and ambient light
				//"LightMode" = "ForwardBase"  had to no commented out 
				"PassFlags" = "OnlyDirectional"
			}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			// Files below include macros and functions to assist with lighting and shadows.
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				//Macro in autolight.cgincm declares as a vector 4 into TEXCOORD2
				SHADOW_COORDS(2)

			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//from auotlight again used for shadows
				TRANSFER_SHADOW(o)
				return o;
			}

			float4 _Color;

			float4 _AmbientColor;

			float4 _SpecularColor;
			float _Glossiness;

			float4  _RimColor;
			float _RimAmount;
			float _RimThreshold;

			float4 frag(v2f i) : SV_Target
			{
				float3 normal = normalize(i.worldNormal);
				float3 viewDir = normalize(i.viewDir);

				// Lighting below is calculated using Blinn-Phong,
				// with values thresholded to creat the "toon" look.
				// https://en.wikipedia.org/wiki/Blinn-Phong_shading_model

				// Calculate illumination from directional light.
				// _WorldSpaceLightPos0 is a vector pointing the OPPOSITE
				// direction of the main directional light.
				float NdotL = dot(_WorldSpaceLightPos0, normal);

				//shadow values passed through
				float shadow = SHADOW_ATTENUATION(i);
				//used to avoid jagged break in lighting transitions
				float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
				//mutliple by main light intenstiy and color
				float4 light = lightIntensity * _LightColor0;

				// getting specular reflection
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);
				//using gloss
				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;

				//Calculate rim lighting
				float rimDot = 1 - dot(viewDir, normal);
				//rim only appear on lit side
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float rim = rimIntensity * _RimColor;

				float4 sample = tex2D(_MainTex, i.uv);


				return (light + _AmbientColor + specular + rim) * _Color * sample;
			}
			ENDCG
		}
			// Shadow casting support.
			UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
		}
	}
