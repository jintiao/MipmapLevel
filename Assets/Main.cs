using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject texturePlane;
    public GameObject shaderPlane;

    private int textureSize = 1024;

    private Color[] mipmapColors = new Color[]
        {
                new Color(1, 0, 0),
                new Color(0, 0, 1),
                new Color(1, 0.5f, 0),
                new Color(1, 0, 0.5f),
                new Color(0, 0.5f, 0.5f),
                new Color(0, 0.25f, 0.5f),
                new Color(0.25f, 0.5f, 0),
                new Color(0.5f, 0, 1),
                new Color(1, 0.25f, 0.5f),
                new Color(0.5f, 0.5f, 0.5f),
                new Color(0.25f, 0.25f, 0.25f),
                new Color(0.125f, 0.125f, 0.125f)
        };

    private GUIStyle textureStyle;

    private void SetupTexturePlane()
    {
        if (texturePlane == null)
            return;

        var renderer = texturePlane.GetComponent<MeshRenderer>();
        if (renderer == null)
            return;

        var texture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, true);
        for (int m = 0; m < texture.mipmapCount; m++)
        {
            var p = texture.GetPixels();
            for (int c = 0; c < p.Length; c++)
                p[c] = mipmapColors[m];
            texture.SetPixels(p, m);
        }
        texture.Apply(false);

        var material = new Material(Shader.Find("Unlit/Texture"));
        material.SetTexture("_MainTex", texture);
        renderer.material = material;
    }

    private void SetupShaderPlane()
    {
        if (shaderPlane == null)
            return;

        var renderer = shaderPlane.GetComponent<MeshRenderer>();
        if (renderer == null)
            return;

        var material = new Material(Shader.Find("Unlit/MipmapColor"));
        material.SetTexture("_MainTex", new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false));
        material.SetColorArray("_MipMapColors", mipmapColors);
        renderer.material = material;
    }

    void Start()
    {
        textureStyle = new GUIStyle { normal = new GUIStyleState { background = Texture2D.whiteTexture } };

        SetupTexturePlane();
        SetupShaderPlane();
    }

    void OnGUI()
    {
        float x = 10;
        float y = 10;
        float width = 200;
        float height = 20;

        var backgroundColor = GUI.backgroundColor;
        for (int i = 0; i < mipmapColors.Length; i++)
        {
            GUI.backgroundColor = mipmapColors[i];
            GUI.Box(new Rect(x, y, width, height), "mipmap " + i, textureStyle);
            y += height * 1.3f;
        }
        GUI.backgroundColor = backgroundColor;
    }
}
