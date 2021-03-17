Shader "Coreficent/Displacement"
{
    Properties
    {
        _MainTex("Base", 2D) = "white" {}
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
