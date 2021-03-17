#ifndef DISTORTION_INCLUDED
#define DISTORTION_INCLUDED

#include "UnityCG.cginc"

sampler2D _MainTex;
float2 _MainTex_TexelSize;

sampler2D _GradientTex;

half4 _Reflection;
float _Speed;
float3 _Strength;

int _DisturbanceCount;
float4 _Disturbances[64];

float distort(float2 position, float2 origin, float time)
{
	float distance = length(position - origin);
	float speed = 0.25;
	float displacement = time - distance / speed;
	return (tex2D(_GradientTex, float2(displacement, 0)).a - 0.5f) * 2;
}

float concat(float2 position)
{
	float result = 0.0;

	for (int i = 0; i < _DisturbanceCount; ++i)
	{
		result += distort(position, _Disturbances[i].xy, _Disturbances[i].z);
	}

	return result;
}

half4 frag(v2f_img i) : SV_Target
{
	float2 position = i.uv * float2(_Speed, 1);

	float distortion = concat(position);

	const float2 derivedX = float2(0.01f, 0);
	const float2 derivedY = float2(0, 0.01f);
	float2 derivedDistortion = float2(concat(position + derivedX) - distortion, concat(position + derivedY) - distortion);

	float2 derivedUV = derivedDistortion * _Strength.xy * 0.2f * _Strength.z;
	half4 color = tex2D(_MainTex, i.uv + derivedUV);
	float reflection = 0.15;
	float finalDistance = pow(length(derivedDistortion) * 2 * reflection, 2);

	return lerp(color, _Reflection, finalDistance);
}

#endif
