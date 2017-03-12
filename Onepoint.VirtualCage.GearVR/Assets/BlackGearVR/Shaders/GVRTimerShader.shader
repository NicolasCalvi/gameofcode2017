Shader "GVRTimerShader"
{
	Properties
	{
		_Texture("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Range("Range", Range(0.0, 1.0)) = 0.5
	}
	SubShader
	{
		Tags
		{ 
			"Queue" = "Geometry" 
			"RenderType" = "Transparent" 
			"IgnoreProjector" = "False" 
		}

		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag 

			#include "UnityCG.cginc"

			// Définition des paramètres 

			sampler2D _Texture;
			float4 _Color;
			float _Range;

			// Structures

			struct vertexInput
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct vertexOuput
			{
				float4 pos : SV_POSITION;
				float2 texcoord : TEXCOORD0;
			};

			// Vertex Shader

			vertexOuput vert(vertexInput v)
			{
				vertexOuput o;

				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;

				return(o);
			}

			// Pixel Shader

			float4 frag(vertexOuput i) : COLOR
			{
				float4 texel = tex2D(_Texture, i.texcoord);
				float level = 1 - texel.r;
				float4 outcolor = float4(_Color.r, _Color.g, _Color.b, texel.a);

				if (level > _Range)
					discard;
			
				return(outcolor);
			}

			ENDCG
		}
	}
}