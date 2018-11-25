﻿Shader "Card/CardTex"
{
	Properties
	{
		_MainTex("Texture Back", 2D) = "white" {}
		alpha("Alpha", Float) = 1
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100
		Cull Back

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
		uniform float alpha;
	

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
		// sample the texture
			fixed4 col = tex2D(_MainTex, i.uv) * alpha;
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
