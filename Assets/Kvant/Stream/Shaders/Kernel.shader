//
// GPGPU kernels for Stream
//
// Texture format:
// .xyz = particle position
// .w   = particle life
//
Shader "Hidden/Kvant/Stream/Kernel"
{
    Properties
    {
        _MainTex("-", 2D) = ""{}
        _EmitterPos("-", Vector) = (0, 0, 20, 0)
    }
    CGINCLUDE

    #pragma multi_compile NOISE_OFF NOISE_ON

    #include "UnityCG.cginc"
    #include "ClassicNoise3D.cginc"

    sampler2D _MainTex;
    float3 _EmitterPos;

    // Pass 0: Initialization
    float4 frag_init(v2f_img i) : SV_Target 
    {
        return float4(1, 0, 0, 1);
    }

    // Pass 1: Update
    float4 frag_update(v2f_img i) : SV_Target 
    {
        float4 p = tex2D(_MainTex, i.uv);
        return p + float4(_EmitterPos, 0);
    }

    ENDCG

    SubShader
    {
        // Pass 0: Initialization
        Pass
        {
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert_img
            #pragma fragment frag_init
            ENDCG
        }
        // Pass 1: Update
        Pass
        {
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert_img
            #pragma fragment frag_update
            ENDCG
        }
    }
}
