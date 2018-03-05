using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VGenMap : MonoBehaviour {
    private Sprite sprite;
    static private Texture2D texture2D;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        
        // world
        World world = World.GetInstance();
        texture2D = texture2D ?? GenerateTexture();

        // sprite
        sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);

        // sprite renderer
        sr = gameObject.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        sr.sprite = sprite;
    }

    // Update is called once per frame
    void Update () {
		
	}

    Texture2D GenerateTexture()
    {
        World world = World.GetInstance();
        Texture2D result = new Texture2D((int)world.GetWorldSize().x, (int)world.GetWorldSize().y, TextureFormat.ARGB32, false)
        {
            filterMode = FilterMode.Point
        };
        Color[] colors = result.GetPixels();
        for (int x = 0; x < result.width; x++)
            for (int y = 0; y < result.height; y++)
            {
                colors[x * result.height + y] = world.GetMapColor(new Vector2(x, y));
            }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
}
