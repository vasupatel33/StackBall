Shader "MyShader/PlayerShader" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_Diffuse ("diffuse", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Specular ("specular", Vector) = (1,1,1,1)
		_Gloss ("gloss", Range(0, 2)) = 0.6
		_SpecularGloss ("SpecularGloss", Float) = 0.4
		_DepthColor ("DepthColor", Vector) = (1,1,1,1)
		_BumpMap ("Bump Map", 2D) = "black" {}
		_BumpScale ("Bump Scale", Range(0.1, 30)) = 10
		_Alpha ("_Alpha", Float) = 0.5
		_Saturation ("_Saturation", Float) = 1
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}