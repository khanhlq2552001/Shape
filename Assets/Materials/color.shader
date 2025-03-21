Shader "Custom/OverlayThenMultiplyOptimized"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MultiplyColor ("Multiply Color", Color) = (1,1,1,1) // Multiply: #ffdc36
        _OverlayColor ("Overlay Color", Color) = (1,1,1,1)  // Overlay: #dd9804
        _BrightnessFactor ("Brightness Factor", Range(1, 2)) = 1.2 // Hệ số tăng sáng
        _OverlayStrength ("Overlay Strength", Range(1, 2)) = 1.2 // Tăng độ đậm Overlay
        _MultiplyBoost ("Multiply Boost", Range(1, 2)) = 1.2 // Làm tươi màu Multiply
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha  // Đảm bảo blending đúng
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MultiplyColor;
            float4 _OverlayColor;
            float _BrightnessFactor;
            float _OverlayStrength;
            float _MultiplyBoost;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Overlay blending chuẩn Photoshop
            fixed3 ApplyOverlay(fixed3 baseColor, fixed3 blendColor, float strength)
            {
                blendColor = saturate(blendColor * strength); // Tăng độ đậm của màu Overlay
                return saturate(lerp(
                    2.0 * baseColor * blendColor,
                    1.0 - 2.0 * (1.0 - baseColor) * (1.0 - blendColor),
                    step(0.5, baseColor)
                ));
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Lấy màu texture
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Áp dụng Overlay trước
                fixed3 overlayResult = ApplyOverlay(texColor.rgb, _OverlayColor.rgb, _OverlayStrength);

                // Sau đó áp dụng Multiply (làm tươi hơn)
                fixed3 finalColor = overlayResult * saturate(_MultiplyColor.rgb * _MultiplyBoost);

                // Làm sáng màu bằng cách pha trộn với trắng
                finalColor = lerp(finalColor, fixed3(1,1,1), 1.0 - (1.0 / _BrightnessFactor));

                return fixed4(finalColor, texColor.a * _MultiplyColor.a);
            }
            ENDCG
        }
    }
}
