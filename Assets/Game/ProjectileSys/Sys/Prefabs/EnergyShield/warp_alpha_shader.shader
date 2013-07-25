Shader "Custom/WarpNormal"
{
	Properties 
	{
_main("_main", 2D) = "black" {}
_normal("_normal", 2D) = "bump" {}
_noise("_noise", 2D) = "black" {}
_specular("_specular", 2D) = "black" {}
_noise_strength("_noise_strength", Range(0,0.5) ) = 0.035
_emission("_emission", Color) = (1,1,1,1)
_alpha("_alpha_", Range(0,1) ) = 0.5

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Transparent"
"IgnoreProjector"="True"
"RenderType"="Transparent"
		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert alpha
#pragma target 2.0


sampler2D _main;
sampler2D _normal;
sampler2D _noise;
sampler2D _specular;
float _noise_strength;
float4 _emission;
float _alpha;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_main;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);


			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Multiply0=_Time * float4( 0.1,0.1,0.1,0.1 );
float4 Add1=(IN.uv_main.xyxy) + Multiply0;
float4 Tex2D1=tex2D(_noise,Add1.xy);
float4 Normalize0=normalize(Tex2D1);
float4 Multiply2=Normalize0 * _noise_strength.xxxx;
float4 Add2=(IN.uv_main.xyxy) + Multiply2;
float4 Tex2D0=tex2D(_main,Add2.xy);
float4 Tex2DNormal0=float4(UnpackNormal( tex2D(_normal,Add2.xy)).xyz, 1.0 );
float4 UnpackNormal0=float4(UnpackNormal(Tex2DNormal0).xyz, 1.0);
float4 Tex2D2=tex2D(_specular,Add2.xy);
float4 Multiply1=_emission * Tex2D2;
float4 Master0_3_NoInput = float4(0,0,0,0);
float4 Master0_4_NoInput = float4(0,0,0,0);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Tex2D0;
o.Normal = UnpackNormal0;
o.Emission = Multiply1;
o.Alpha = _alpha.xxxx;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}