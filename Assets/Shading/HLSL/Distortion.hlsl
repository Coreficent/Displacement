#ifndef DISTORTION_INCLUDED
#define DISTORTION_INCLUDED

#include "UnityCG.cginc"

sampler2D _MainTex;
float2 _MainTex_TexelSize;

sampler2D _GradientTex;

half4 _Reflection;
float4 _Speed;    // [ aspect, 1, scale, 0 ]
float4 _Strength;    // [ 1, 1/aspect, refraction, reflection ]


int _DisturbanceCount;
float4 _Disturbance[64];

float wave(float2 position, float2 origin, float time)
{
	float d = length(position - origin);
	float t = time - d * _Speed.z;
	return (tex2D(_GradientTex, float2(t, 0)).a - 0.5f) * 2;
}

float allwave(float2 position)
{
	float result = 0.0;

	for (int i = 0; i < _DisturbanceCount; ++i)
	{
		result += wave(position, _Disturbance[i].xy, _Disturbance[i].z);
	}

	return result;
}

half4 frag(v2f_img i) : SV_Target
{
	const float2 dx = float2(0.01f, 0);
	const float2 dy = float2(0, 0.01f);

	float2 p = i.uv * _Speed.xy;

	float w = allwave(p);
	float2 dw = float2(allwave(p + dx) - w, allwave(p + dy) - w);

	float2 duv = dw * _Strength.xy * 0.2f * _Strength.z;
	half4 c = tex2D(_MainTex, i.uv + duv);
	float fr = pow(length(dw) * 3 * _Strength.w, 3);

	return lerp(c, _Reflection, fr);
}

#endif
