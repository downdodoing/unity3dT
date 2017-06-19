// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "SelfDefine/UnlitTransparentCutoutHitShader"
{
	Properties
	{
	    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	    _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	    _Alpha("Alpha", Range(0,1)) = 1
	    _HitColor ("Hit (RGB)", Color) = (0,0,0,0)
	    _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _Outline ("Outline Width", Range (0.000, 0.03)) = 0
        //DirectLight=========begin
        _LightPos ("LightPos", Vector) = (0,0,0,0)
	  	_LightColor ("LightColor", Color) = (0,0,0,0)
	  	_LightIntensity ("LightIntensity", Range(0, 8)) = 1
        //DirectLight=========end
        
        //Emission=========begin
//        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
		//Emission=========end
	}

	SubShader
	{
		//Emission=========begin
//		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
//		LOD 200
//    	Blend SrcAlpha OneMinusSrcAlpha
//    	Fog { Mode Off }
//    	CGPROGRAM
//    	#pragma surface surf Lambert noambient
//    	
//    	sampler2D _MainTex;
//    	fixed _Cutoff;
//    	half _Alpha;
//    	float4 _HitColor;
//    	float4 _EmissionColor;
//    	
//    	struct Input
//    	{
//			float2 uv_MainTex;
//		};
//		
//		void surf (Input IN, inout SurfaceOutput o)
//		{
//			half4 tex = tex2D(_MainTex, IN.uv_MainTex);
//			clip(tex.a - _Cutoff);
//			//
//			o.Albedo = tex.rgb + _HitColor.rgb;
//			o.Emission = (tex.rgb * _EmissionColor.rgb) + _HitColor.rgb;
//			o.Alpha = tex.a * _Alpha;
//		}
//    	ENDCG
    	//Emission=========end
    	
    	//DirectLight=========begin
    	Tags {"Queue"="Geometry" "RenderType" = "Opaque" }
    	LOD 20
    	Fog { Mode Off }
    	Blend SrcAlpha OneMinusSrcAlpha
    	
    	Pass
    	{
    		Lighting Off
    		
    		CGPROGRAM
    		//
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			//
			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 color : TEXCOORD1;
          	};

			struct vert_data
			{
				float4 vertex : POSITION;
				float2 texcoord0 : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			sampler2D _MainTex;
			fixed _Cutoff;
		  	float4 _LightPos;
		  	half4 _LightColor;
		  	half _LightIntensity;
		  	half _Alpha;
    		float4 _HitColor;
		  	//
			v2f vert (vert_data v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord0;
				//
				_LightPos.w = 0;
				float4 ligthDirW = normalize(_LightPos);
				float4 ligthDirO = mul(ligthDirW, unity_ObjectToWorld);
				o.color = saturate(dot(ligthDirO.xyz, v.normal)) * _LightColor * _LightIntensity;
				//
				return o;
			}

			float4 frag (v2f i) : COLOR
			{
				float4 texCol = tex2D(_MainTex,i.uv);
				clip(texCol.a - _Cutoff);
				texCol.rgb = texCol.rgb + texCol.rgb * i.color.rgb * texCol.a + _HitColor;
				texCol.a *= _Alpha;
				return texCol;
			}
			//
			ENDCG
    	}
    	//DirectLight=========end
    	
    	Pass
		{
			Name "OUTLINE"
			Tags { "LightMode" = "Always"}
			
			Cull front
			ZWrite Off
			Fog { Mode Off }
			
			CGPROGRAM
			//
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			//
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Cutoff;
			fixed _Alpha;
			uniform float _Outline;
			uniform float4 _OutlineColor;
			//
			struct v2f
			{
				float4 pos : POSITION;
				float4 color : COLOR;
				half2 texcoord : TEXCOORD0;
			};
			//
			v2f vert (appdata_full v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				//
				float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float2 offset = TransformViewToProjection(norm.xy);
				o.pos.xy += offset * o.pos.z * _Outline;
				o.color = _OutlineColor;
				//
				return o;
			}
			
			float4 frag (v2f i) : COLOR
			{
				fixed4 outColor = tex2D(_MainTex, i.texcoord);
				clip(outColor.a - _Cutoff);
				//
				if (0 >= _Outline)
				{
					return outColor * float4(1,1,1,0);
				}
				else
				{
					return i.color * float4(1,1,1,_Alpha);
				}
			}
			//
			ENDCG
		}
	}






//	Properties
//	{
//	    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
//	    _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
//	    _HitColor ("Hit (RGB)", Color) = (0,0,0,0)
//	    _Alpha("Alpha", Range(0,1)) = 1
//	    _OutlineColor ("Outline Color", Color) = (0,0,0,1)
//        _Outline ("Outline Width", Range (0.000, 0.03)) = 0
//	}
//	
//	SubShader
//	{
//		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
//		LOD 100
//
//		Lighting Off
//		Blend SrcAlpha OneMinusSrcAlpha
//
//		Pass
//		{
//			CGPROGRAM
//			//
//			#pragma vertex vert
//			#pragma fragment frag
//
//			#include "UnityCG.cginc"
//
//			struct appdata_t
//			{
//				float4 vertex : POSITION;
//				float2 texcoord : TEXCOORD0;
//			};
//
//			struct v2f
//			{
//				float4 vertex : SV_POSITION;
//				half2 texcoord : TEXCOORD0;
//			};
//
//			sampler2D _MainTex;
//			float4 _MainTex_ST;
//			fixed _Cutoff;
//			float4 _HitColor;
//			fixed _Alpha;
//
//			v2f vert (appdata_t v)
//			{
//				v2f o;
//				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
//				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
//				return o;
//			}
//
//			fixed4 frag (v2f i) : COLOR
//			{
//				fixed4 outColor = tex2D(_MainTex, i.texcoord);
//				clip(outColor.a - _Cutoff);
//				return (outColor + _HitColor) * float4(1,1,1,_Alpha);
//			}
//			//
//			ENDCG
//		}
//		
//		Pass
//		{
//			Name "OUTLINE"
//			Tags { "LightMode" = "Always"}
//			
//			Cull front
//			ZWrite Off
//			
//			CGPROGRAM
//			//
//			#pragma vertex vert
//			#pragma fragment frag
//			
//			#include "UnityCG.cginc"
//			//
//			sampler2D _MainTex;
//			float4 _MainTex_ST;
//			fixed _Cutoff;
//			fixed _Alpha;
//			uniform float _Outline;
//			uniform float4 _OutlineColor;
//			//
//			struct v2f
//			{
//				float4 pos : POSITION;
//				float4 color : COLOR;
//				half2 texcoord : TEXCOORD0;
//			};
//			//
//			v2f vert (appdata_full v)
//			{
//				v2f o;
//				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
//				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
//				//
//				float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
//				float2 offset = TransformViewToProjection(norm.xy);
//				o.pos.xy += offset * o.pos.z * _Outline;
//				o.color = _OutlineColor;
//				//
//				return o;
//			}
//			
//			float4 frag (v2f i) : COLOR
//			{
//				fixed4 outColor = tex2D(_MainTex, i.texcoord);
//				clip(outColor.a - _Cutoff);
//				//
//				if (0 >= _Outline)
//				{
//					return outColor * float4(1,1,1,0);
//				}
//				else
//				{
//					return i.color * float4(1,1,1,_Alpha);
//				}
//			}
//			//
//			ENDCG
//		}
//	}
}