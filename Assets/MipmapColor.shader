Shader "Unlit/MipmapColor"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
		_LodScale("Lod Scale", Float) = 0.5
		_LodBias("Lod Bias", Float) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

			float4 _MainTex_TexelSize;
			float _LodScale;
			float _LodBias;
			float4 _MipMapColors[11];

            fixed4 frag (v2f_img i) : SV_Target
            {
				float2 dx = ddx(i.uv) * _MainTex_TexelSize.zw;
				float2 dy = ddy(i.uv) * _MainTex_TexelSize.zw;
				float p = max(dot(dx, dx), dot(dy, dy));
				int mip = clamp(int(_LodScale * log2(p) + _LodBias), 0, 11);

                fixed4 col = _MipMapColors[mip];
                return col;
            }
            ENDCG
        }
    }
}
