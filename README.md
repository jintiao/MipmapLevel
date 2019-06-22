# MipmapLevel

calculate texture mipmap level in fragment shader.

shader code
```c++
float4 _MipMapColors[11];
float4 _MainTex_TexelSize;

fixed4 frag (v2f_img i) : SV_Target
{
    float2 uv = i.uv * _MainTex_TexelSize.zw;
    float2 dx = ddx(uv);
    float2 dy = ddy(uv);
    float rho = max(dot(dx, dx), dot(dy, dy));
    float lambda = 0.5 * log2(rho);
    int d = max(int(lambda + 0.5), 0);
    return _MipMapColors[d];
}
```

hardware mipmap level(left) vs shader mipmap level(right)
![mipmap level](/Image/hardware-vs-shader.png)
