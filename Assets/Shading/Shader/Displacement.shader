﻿Shader "Coreficent/Displacement"
{
    Properties
    {
        _MainTex("Base", 2D) = "white" {}
        _GradientTex("Gradient", 2D) = "white" {}
        _Reflection("Reflection Color", Color) = (0, 0, 0, 0)
        _Params1("Parameters 1", Vector) = (1, 1, 0.8, 0)
        _Params2("Parameters 2", Vector) = (1, 1, 1, 0)
    }

        SubShader
        {
            Pass
            {
                ZTest Always Cull Off ZWrite Off
                Fog { Mode off }
                CGPROGRAM
                #pragma fragmentoption ARB_precision_hint_fastest 
                #pragma target 3.0
                #pragma vertex vert_img
                #pragma fragment frag

                #include "../HLSL/Distortion.hlsl"

                ENDCG
            }
        }
}
