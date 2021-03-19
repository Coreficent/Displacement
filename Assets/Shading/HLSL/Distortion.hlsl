#ifndef DISTORTION_INCLUDED
#define DISTORTION_INCLUDED

#include "UnityCG.cginc"

sampler2D _MainTex;
sampler2D _GradientTex;

float _Aspect;
float _Strength;

int _DisturbanceCount;
float4 _Disturbances[64];

/*
* calculates a high map as a function of the origin and the current position based on the input wave function.
*/

float concat(float2 position)
{
	float result = 0.0f;

	for (int i = 0; i < _DisturbanceCount; ++i)
	{
		float2 origin = _Disturbances[i].xy;
		float time = _Disturbances[i].z;
		float distance = length(position - origin);
		float speed = 0.25f;
		float displacement = time - distance / speed;
		float distort = (tex2D(_GradientTex, float2(displacement, 0.0f)).a - 0.5f) * 2.0f;

		result += distort;
	}

	return result;
}

/*
* converts the height map after the concatenation to a displacement map to create the ripple effect. 
*/

half4 frag(v2f_img i) : SV_Target
{
	float2 position = i.uv * float2(_Aspect, 1.0f);
	float distortion = concat(position);

	float2 derivedX = float2(0.01f, 0.0f);
	float2 derivedY = float2(0.0f, 0.01f);

	float2 derivedDistortion = float2(concat(position + derivedX) - distortion, concat(position + derivedY) - distortion);
	float2 derivedUV = derivedDistortion * float2(1.0f, 1.0f/ _Aspect) * 0.2f * _Strength;

	half4 color = tex2D(_MainTex, i.uv + derivedUV);

	float reflection = 0.15f;
	float finalDistance = pow(length(derivedDistortion) * 2.0f * reflection, 2.0f);

	return lerp(color, half4(0.5, 0.5, 0.5, 0.1), finalDistance);
}

#endif
