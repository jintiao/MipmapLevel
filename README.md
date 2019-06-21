# MipmapLevel

calculate texture mipmap level in fragment shader.

```c++
float2 dx = ddx(i.uv) * _MainTex_TexelSize.zw;
float2 dy = ddy(i.uv) * _MainTex_TexelSize.zw;
float p = max(dot(dx, dx), dot(dy, dy));
int mip = clamp(int(_LodScale * log2(p) + _LodBias), 0, 11);
```
