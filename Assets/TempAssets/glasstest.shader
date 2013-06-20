Shader "TestGlass"
{
	Properties 
	{
_Maincolor("_Maincolor", Color) = (1,1,1,1)
_TransparentTexture("_TransparentTexture", 2D) = "black" {}
_Reflective("_Reflective", Cube) = "black" {}
_TransparentAmount("_TransparentAmount", Range(0,1) ) = 0.5
_ReflectiveAmount("ReflectiveAmount", Range(0,1) ) = 0.5
_SpecularColor("SpecularColor", Color) = (1,1,1,1)
_Shininess("Shininess", Range(0,1) ) = 0.5

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Transparent"
"IgnoreProjector"="False"
"RenderType"="Transparent"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  alpha decal:blend vertex:vert
#pragma target 2.0


float4 _Maincolor;
sampler2D _TransparentTexture;
samplerCUBE _Reflective;
float _TransparentAmount;
float _ReflectiveAmount;
float4 _SpecularColor;
float _Shininess;

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
				float2 uv_TransparentTexture;
float3 simpleWorldRefl;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);

o.simpleWorldRefl = -reflect( normalize(WorldSpaceViewDir(v.vertex)), normalize(mul((float3x3)_Object2World, SCALED_NORMAL)));

			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Tex2D0=tex2D(_TransparentTexture,(IN.uv_TransparentTexture.xyxy).xy);
float4 Multiply0=_Maincolor * Tex2D0;
float4 TexCUBE0=texCUBE(_Reflective,float4( IN.simpleWorldRefl.x, IN.simpleWorldRefl.y,IN.simpleWorldRefl.z,1.0 ));
float4 Multiply3=_ReflectiveAmount.xxxx * TexCUBE0;
float4 Add0=Multiply0 + Multiply3;
float4 Multiply2=_SpecularColor * _Shininess.xxxx;
float4 Multiply1=Tex2D0.aaaa * _TransparentAmount.xxxx;
float4 Master0_1_NoInput = float4(0,0,1,1);
float4 Master0_2_NoInput = float4(0,0,0,0);
float4 Master0_4_NoInput = float4(0,0,0,0);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Add0;
o.Specular = Multiply2;
o.Alpha = Multiply1;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}