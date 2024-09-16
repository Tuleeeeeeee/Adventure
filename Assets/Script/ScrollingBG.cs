using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ScrollingBG : MonoBehaviour
{
    // Start is called before the first frame update
    private TilemapRenderer tilemapRenderer;
    [SerializeField]
    public Vector2 scrollSpeed = new Vector2(0.5f, 0f);
    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = Time.time * scrollSpeed;
        tilemapRenderer.material.mainTextureOffset = offset;


    }
}
