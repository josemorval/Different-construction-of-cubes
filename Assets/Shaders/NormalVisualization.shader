Shader "Unlit/NormalVisualization"
{

	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal: NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 normal : TEXCOORD0;
			};


			float _zoffset;
			float4x4 _rotMatrix;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(_Object2World,mul(_rotMatrix, v.vertex)) + float4(0.0,0.0,_zoffset,0.0);
				o.vertex = mul(UNITY_MATRIX_VP,o.vertex);
				o.normal = v.normal;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return float4(0.5+0.5*i.normal.x,0.5+0.5*i.normal.y,0.5+0.5*i.normal.z,1.0);
			}
			ENDCG
		}
	}
}
