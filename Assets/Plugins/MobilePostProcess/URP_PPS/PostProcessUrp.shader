Shader "SupGames/Mobile/PostProcessURP"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "" {}
	}

	HLSLINCLUDE

	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
	#define lum half3(0.212673h, 0.715152h, 0.072175h)
	#define hal half3(0.5h, 0.5h, 0.5h)

	uniform TEXTURE2D_X(_MainTex);
	SAMPLER(sampler_MainTex);
	uniform TEXTURE3D(_LutTex);
	SAMPLER(sampler_LutTex);
	uniform TEXTURE2D_X(_MaskTex);
	SAMPLER(sampler_MaskTex);
	uniform TEXTURE2D_X(_BlurTex);
	SAMPLER(sampler_BlurTex);
	uniform half _LutAmount;
	uniform half4 _BloomColor;
	uniform half _BlurAmount;
	uniform half _BloomDiffuse;
	uniform half4 _Color;
	uniform half4 _BloomData;
	uniform half _Contrast;
	uniform half _Brightness;
	uniform half _Saturation;
	uniform half _CentralFactor;
	uniform half _SideFactor;
	uniform half _Offset;
	uniform half _FishEye;
	uniform half _LensDistortion;
	uniform half4 _VignetteColor;
	uniform half _VignetteAmount;
	uniform half _VignetteSoftness;
	uniform half4 _MainTex_TexelSize;

	struct appdata
	{
		half4 pos : POSITION;
		half2 uv : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2fb
	{
		half4 pos : SV_POSITION;
		half4 uv : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
		UNITY_VERTEX_OUTPUT_STEREO
	};
	struct v2f {
		half4 pos : SV_POSITION;
		half4 uv : TEXCOORD0;
		half4 uv1 : TEXCOORD1;
		half4 uv2 : TEXCOORD2;
		half2 uv3 : TEXCOORD3;
		UNITY_VERTEX_INPUT_INSTANCE_ID
		UNITY_VERTEX_OUTPUT_STEREO
	};

	v2fb vertBlur(appdata i)
	{
		v2fb o;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_TRANSFER_INSTANCE_ID(i, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, half4(i.pos.xyz, 1.0h)));
		i.uv = UnityStereoTransformScreenSpaceTex(i.uv);
#if defined(BLOOM) && !defined(BLUR)
		half2 offset = _MainTex_TexelSize.xy * _BloomDiffuse;
#else
		half2 offset = _MainTex_TexelSize.xy * _BlurAmount;
#endif
		o.uv = half4(i.uv - offset, i.uv + offset);
		return o;
	}

	v2f vert(appdata i)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_TRANSFER_INSTANCE_ID(i, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, half4(i.pos.xyz, 1.0h)));
		o.uv.xy = UnityStereoTransformScreenSpaceTex(i.uv);
		o.uv.zw = i.uv;
		o.uv1 = half4(o.uv.xy - _MainTex_TexelSize.xy, o.uv.xy + _MainTex_TexelSize.xy);
		o.uv2.x = o.uv.x - _Offset * _MainTex_TexelSize.x - 0.5h;
		o.uv2.y = o.uv.x + _Offset * _MainTex_TexelSize.x - 0.5h;
		o.uv2.zw = i.uv - 0.5h;
		o.uv3 = o.uv.xy - 0.5h;
		return o;
	}

	half4 fragBlur(v2fb i) : SV_Target
	{
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
		half4 b = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.xy);
		b += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.xw);
		b += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.zy);
		b += SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.zw);
		return b * 0.25h;
	}

		half4 frag(v2f i) : SV_Target
	{
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

		half q = dot(i.uv2.zw, i.uv2.zw);
		half q2 = sqrt(q);

#ifdef DISTORTION
		half q3 = q * _LensDistortion * q2;
		i.uv.xy = (1.0h + q3) * i.uv3 + hal;
#endif

		half4 c = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv.xy);

#if defined(BLUR) || defined(BLOOM)
		half4 b = SAMPLE_TEXTURE2D_X(_BlurTex, sampler_BlurTex, i.uv.xy);
#endif

#ifdef BLUR
		half4 m = SAMPLE_TEXTURE2D_X(_MaskTex, sampler_MaskTex, i.uv.zw);
#endif

#ifdef CHROMA
		half r = dot(i.uv2.xw, i.uv2.xw);
#ifdef DISTORTION
		half2 r2 = (1.0h + r * _FishEye * sqrt(r) + q3) * i.uv2.xw + hal;
#else
		half2 r2 = (1.0h + r * _FishEye * sqrt(r)) * i.uv2.xw + hal;
#endif
		c.r = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, r2).r;
#ifdef BLUR 
		b.r = SAMPLE_TEXTURE2D_X(_BlurTex, sampler_BlurTex, r2).r;
#endif
		r = dot(i.uv2.yw, i.uv2.yw);
#ifdef DISTORTION
		r2 = (1.0h - r * _FishEye * sqrt(r) + q3) * i.uv2.yw + hal;
#else
		r2 = (1.0h - r * _FishEye * sqrt(r)) * i.uv2.yw + hal;
#endif
		c.b = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, r2).b;
#ifdef BLUR
		b.b = SAMPLE_TEXTURE2D_X(_BlurTex, sampler_BlurTex, r2).b;
#endif
#endif
#if !defined(UNITY_NO_LINEAR_COLORSPACE)
		c.rgb = sqrt(c.rgb);
#if defined(BLOOM)|| defined(BLUR)
		b.rgb = sqrt(b.rgb);
#endif
#endif
#ifdef SHARPEN
		c *= _CentralFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.xy) * _SideFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.xw) * _SideFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.zy) * _SideFactor;
		c -= SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, i.uv1.zw) * _SideFactor;
#endif

#ifdef LUT
		c = lerp(c, SAMPLE_TEXTURE3D(_LutTex, sampler_LutTex, c.rgb * 0.9375h + 0.03125h), _LutAmount);
#if defined(BLOOM)|| defined(BLUR)
		b = lerp(b, SAMPLE_TEXTURE3D(_LutTex, sampler_LutTex, b.rgb * 0.9375h + 0.03125h), _LutAmount);
#endif
#endif

#ifdef BLUR
		c = lerp(c, b, m.r);
#endif
#ifdef BLOOM
		half br = max(b.r, max(b.g, b.b));
		half soft = clamp(br - _BloomData.y, 0.0h, _BloomData.z);
		b *= max(soft * soft * _BloomData.w, br - _BloomData.x) / max(br, 0.00001h) ;
#if !defined(UNITY_NO_LINEAR_COLORSPACE)
		b.rgb *= b.rgb;
#endif
		c += b * _BloomColor;
#endif

#ifdef FILTER
		c.rgb = _Contrast * c.rgb + _Brightness;
		c.rgb = lerp(dot(c.rgb, lum), c.rgb, _Saturation) * _Color.rgb;
#endif
		c.rgb = lerp(_VignetteColor.rgb, c.rgb, smoothstep(_VignetteAmount, _VignetteSoftness, q2));

#if !defined(UNITY_NO_LINEAR_COLORSPACE)
		c.rgb *= c.rgb;
#endif
		return c;
	}
	ENDHLSL


	Subshader
	{
		Pass //0
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			HLSLPROGRAM
			#pragma vertex vertBlur
			#pragma fragment fragBlur
			#pragma shader_feature_local BLOOM
			#pragma shader_feature_local BLUR
			ENDHLSL
		}

		Pass //1
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature_local BLOOM
			#pragma shader_feature_local BLUR
			#pragma shader_feature_local CHROMA
			#pragma shader_feature_local LUT
			#pragma shader_feature_local FILTER
			#pragma shader_feature_local SHARPEN
			#pragma shader_feature_local DISTORTION
			ENDHLSL
		}
	}
	Fallback off
}