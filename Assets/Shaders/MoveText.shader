Shader "Unlit/MoveText"
{
    Properties
    {
        _MainText ("Main Texture", 2D) = "white" {}

        _Move ("Move", Range(0,1)) = 0.5
        _Scale ("Scale", Range(0,1)) = 0.5

        _Color ("Color", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainText;
            float4 _MainText_ST;

            float _Move;
            half _Scale;
            
            float4 _Color;
            float4 _Color2;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.zw = TRANSFORM_TEX(v.uv, _MainText);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed mainText = tex2D(_MainText, i.uv.zw * lerp(1, 1 + _Scale, cos(_Time.y)) + (_Move * sin(_Time.y))).a;

                fixed4 color = lerp(_Color2, _Color, mainText);

                return color;
            }

            ENDCG
        }
    }
}
