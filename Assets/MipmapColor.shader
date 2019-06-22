Shader "Unlit/MipmapColor"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
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

			float4 _MipMapColors[11];
			float4 _MainTex_TexelSize;

            fixed4 frag (v2f_img i) : SV_Target
            {
            	float uv = i.uv * _MainTex_TexelSize.zw;
            	float dx = ddx(uv);
            	float dy = ddy(uv);
#if 0
            	float rho = max(sqrt(dot(dx, dx)), sqrt(dot(dy, dy)));
            	float lambda = log2(rho);
#else
				float rho = max(dot(dx, dx), dot(dy, dy));
				float lambda = 0.5 * log2(rho);
#endif
            	int d = max(int(lambda + 0.5), 0);
                return _MipMapColors[d];
            }
            ENDCG
        }
    }
}
