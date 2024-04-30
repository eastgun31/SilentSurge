Shader "Custom/testshader1"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Cutoff("AlphaTest cutoff", Range(0,1)) = 0.5

    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 200

        zwrite Off

        CGPROGRAM
        #pragma surface surf Standard alpha:blend
        #pragma target 3.0

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
        };


        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex)* _Color;
            o.Albedo = c.rgb;
            o.Alpha = 0.5;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
