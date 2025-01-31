﻿Shader "Custom/SnowShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SnowColor ("Snow Color", Color) = (1,1,1,1)
		_SnowTex ("Snow Texture", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Cutoff ("Snow Angle Cutoff", Range(0,1)) = 0.1
		_Strength("Snow Angle Strength", Float) = 7.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SnowTex;

		struct Input {
			float2 uv_SnowTex;
			float2 uv_MainTex;
			float3 worldNormal; INTERNAL_DATA
			float3 worldPos;
		};


		half _Cutoff;
		half _Strength;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _SnowColor;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float3 worldNormal = WorldNormalVector(IN, o.Normal);
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			fixed4 snow = tex2D (_SnowTex, IN.uv_SnowTex) * _SnowColor;

			//snow effect based on normals
			float upDot = clamp( dot(worldNormal, float3(0,1,0)) , 0.0, 1.0);
			float snowAmount = clamp( (upDot - _Cutoff) * _Strength, 0.0, 1.0 );
			c = lerp(c, snow, snowAmount);
			c.a = 1;
			

			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
