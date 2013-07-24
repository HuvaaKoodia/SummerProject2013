Shader "Custom/TransparentIlluminateBackfacedClean" {
	Properties {
		_Diffuse("_Diffuse", 2D) = "gray" {}
		_Illu("_Illu", 2D) = "black" {}
		_IlluColor("_IlluColor", Color) = (1,1,1,1)
	}
	SubShader {
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="False"
			"RenderType"="Opaque"
		}
		LOD 200
		Cull Off
		ZWrite On
		ZTest LEqual
		ColorMask RGBA
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _Diffuse;
		sampler2D _Illu;
		float4 _IlluColor;

		struct Input {
			float2 uv_Diffuse;
			float2 uv_Illu;
		};

		void surf (Input IN, inout SurfaceOutput o) {
				float4 diff=tex2D(_Diffuse,(IN.uv_Diffuse.xyxy).xy);
				float4 illu=tex2D(_Illu,(IN.uv_Illu.xyxy).xy);
				
				o.Normal = float3(0.0,0.0,1.0);
				o.Albedo = diff;
				o.Emission = diff* _IlluColor*illu;
				o.Alpha = diff.aaaa;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
