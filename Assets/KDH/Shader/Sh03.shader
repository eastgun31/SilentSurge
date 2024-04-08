Shader "Custom/Sh03"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _SecondTex("Albedo (RGB)", 2D) = "white" {}
        _ratio("Ratio", Range(0,1)) = 0
        _a("A", Range(0,1)) = 0 // 임의의 변수 추가 및 inspector에 시각화
    }
        SubShader
    {
        Tags{"RenderType" = "Transparent""Queue" = "Transparent"}

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        sampler2D _MainTex;
        sampler2D _SecondTex;
        float _ratio;
        float _a; 

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_SecondTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //물결 쉐이더
            //float2 uv = IN.uv_SecondTex;
            //fixed4 d = tex2D(_SecondTex, float2(uv.x, uv.y -_Time.y));

            //float u = IN.uv_MainTex.x + d.r * _SinTime.w;
            //float v = IN.uv_MainTex.y + d.r * _Time.y;
            //fixed4 c = tex2D(_MainTex, float2(u, v));
            
            float u = IN.uv_MainTex.x + _a; // 임의 변수 수치로 메인 텍스쳐 x값 이동
            float v = IN.uv_MainTex.y;
            fixed4 c = tex2D(_MainTex, float2(u,v));

            fixed4 d = tex2D(_SecondTex, IN.uv_SecondTex);

            o.Albedo = lerp(c.rgb, d.rgb, _ratio);
            o.Alpha = c.a;
        }
        ENDCG
    }
}
