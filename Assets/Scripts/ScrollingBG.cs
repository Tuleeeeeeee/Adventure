using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ScrollingBG : MonoBehaviour
{
    // Start is called before the first frame update
    private TilemapRenderer tilemapRenderer;
    private Vector2 scrollSpeed = new Vector2(1f, 1f);
    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }
    void FixedUpdate()
    {
        Vector2 offset = Time.time * scrollSpeed;
        tilemapRenderer.material.mainTextureOffset = offset;
    }
}
