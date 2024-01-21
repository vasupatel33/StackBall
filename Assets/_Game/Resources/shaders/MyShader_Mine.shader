Shader "MyShader/Mine" {
	Properties {
		_Tint ("Color Tint", Vector) = (1,1,1,1)
		_MainTex ("Main Tex", 2D) = "white" {}
		_Normal ("Normal Tex", 2D) = "bump" {}
		_BumpScale ("Bump Scale", Float) = 1
		_Specular ("Specular", Vector) = (1,1,1,1)
		_Gloss ("Gloss", Range(8, 256)) = 20
		_HalfScale ("HalfScale", Float) = 0.5
		_HalfHeight ("HalfHeight", Float) = 0.5
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
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}