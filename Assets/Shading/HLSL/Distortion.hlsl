#ifndef DISTORTION_INCLUDED
#define DISTORTION_INCLUDED

#include "UnityCG.cginc"

sampler2D _MainTex;
float2 _MainTex_TexelSize;

sampler2D _GradientTex;

half4 _Reflection;
float4 _Speed;
float4 _Strength;

int _DisturbanceCount;
float4 _Disturbances[64];

float wave(float2 position, float2 origin, float time)
{
	float distance = length(position - origin);
	float displacement = time - distance * _Speed.z;
	return (tex2D(_GradientTex, float2(displacement, 0)).a - 0.5f) * 2;
}

float concat(float2 position)
{
	float result = 0.0;

	for (int i = 0; i < _DisturbanceCount; ++i)
	{
		result += wave(position, _Disturbances[i].xy, _Disturbances[i].z);
	}

	return result;
}

half4 frag(v2f_img i) : SV_Target
{
	const float2 dx = float2(0.01f, 0);
	const float2 dy = float2(0, 0.01f);

	float2 p = i.uv * _Speed.xy;

	float w = concat(p);
	float2 dw = float2(concat(p + dx) - w, concat(p + dy) - w);

	float2 duv = dw * _Strength.xy * 0.2f * _Strength.z;
	half4 c = tex2D(_MainTex, i.uv + duv);
	float fr = pow(length(dw) * 3 * _Strength.w, 3);

	return lerp(c, _Reflection, fr);
}

#endif
