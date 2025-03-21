Shader "Custom/MultiplyShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // Texture của sprite
        _MultiplyColor ("Multiply Color", Color) = (1,1,1,1) // Màu Multiply
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
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

            sampler2D _MainTex; // Texture của sprite
            float4 _MultiplyColor; // Màu Multiply

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Lấy màu từ texture
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Áp dụng premultiplied alpha
                texColor.rgb *= texColor.a;

                // Áp dụng chế độ Multiply
                fixed4 result = texColor * _MultiplyColor;

                // Khôi phục alpha
                result.a = texColor.a;

                return result;
            }
            ENDCG
        }
    }
}