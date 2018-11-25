Shader "Card/CardRim"
{
	Properties
	{
		_MainTex ("Texture Back", 2D) = "white" {}
		_ContentTex("Texture with Words", 2D) = "white" {}
		_ContentTypeTex("Type Texture with Words", 2D) = "white" {}
		_TypeTex("Texture of Type", 2D) = "white" {}
		_TypeDegree("Show Type", Float) = 0
		_Color("Rim Color", Color) = (0,0.5,0.9,1)
		_Width("Rim Width", Range(0,0.5)) = 0.4
		_Degree("Rim Degree", Range(0,1)) = 0
		_isBack("Show on witch side", Float) = 1
		biasX("bias X", Float) = 1
		biasY("bias Y", Float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
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
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _ContentTex;
			float4 _ContentTex_ST;
			sampler2D _ContentTypeTex;
			float4 _ContentTypeTex_ST;
			float4 _Color;
			float _Width;
			float _Degree;
			float _isBack;
			float _TypeDegree;
			sampler2D _TypeTex;
			uniform float biasX;
			uniform float biasY;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex); 
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 typeCol = tex2D(_TypeTex, float2(i.uv.x * biasX, i.uv.y * biasY));
				fixed4 backCol = typeCol * _TypeDegree + (1-_TypeDegree * typeCol.a) * tex2D(_MainTex, float2(i.uv.x * biasX, i.uv.y * biasY));
				fixed4 frontTypeCol = tex2D(_ContentTypeTex, float2(1 - i.uv.x * biasX, i.uv.y * biasY));
				fixed4 frontCol = (1 - frontTypeCol.a) * tex2D(_ContentTex, float2(1 - i.uv.x * biasX, i.uv.y * biasY)) + frontTypeCol * frontTypeCol.a;
				fixed4 col = _isBack > 0.5 ? backCol : frontCol;
				fixed alpha = clamp((max(abs(i.uv.x * biasX - 0.5), abs(i.uv.y * biasY - 0.5)) - (0.5 - _Width)) * (1 / _Width ) - (1-_Degree), 0, 1);
				col = col * (1 - alpha) + _Color * alpha;
				if (col.a < 0.5f)
				{
					discard;
				}
				return col;
			}
			ENDCG
		}
	}
}
