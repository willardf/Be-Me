Shader "Unlit/TextShader"
{
	Properties
	{
		_MainTex ("Font Texture", 2D) = "white" {}
		_InputTex ("Input Texture", 2D) = "white" {}
		_ColorTex ("Input Colors", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

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
			
			sampler2D _InputTex;
			float4 _InputTex_TexelSize;

			sampler2D _ColorTex;

			fixed4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				uint fontNum = tex2D(_InputTex, i.uv).r * 256;

				float uvX = (fontNum % 16) / 16.0;
				float uvY = 1 - (fontNum / 16 + 1) / 16.0;

				float whichX = i.uv.x * _InputTex_TexelSize.z;
				whichX = whichX % 1.0;

				float whichY = i.uv.y * _InputTex_TexelSize.w;
				whichY = whichY % 1.0;

				float2 uv = float2(uvX + whichX / 16.0, uvY + whichY / 16);

				fixed4 col = fixed4(tex2D(_ColorTex, i.uv).rgb, tex2D(_MainTex, uv).a);
				if (col.a > .001) return col;
				discard;
				return fixed4(0,0,0,0);
			}
			ENDCG
		}
	}
}
