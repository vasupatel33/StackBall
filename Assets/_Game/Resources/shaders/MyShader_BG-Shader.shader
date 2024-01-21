Shader "MyShader/BG-Shader" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_Diffuse ("diffuse", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Specular ("specular", Vector) = (1,1,1,1)
		_Gloss ("gloss", Range(0, 2)) = 0.6
		_DepthColor ("DepthColor", Vector) = (1,1,1,1)
		_DepusScale ("DepusScale", Float) = 1
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