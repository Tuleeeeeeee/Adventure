using System;
using System.Collections;
using UnityEngine;

public class GrowSmoothMultiBlock : MonoBehaviour
{
    public GameObject[] tiles;  // Assign all your tile objects here
    [Range(0f, 5f)]
    public float shrinkTime = 0.75f;  // Faster shrinking (reduced time)
    [Range(0f, 5f)]
    public float delayBetweenShrinks = 0.05f;  // Less delay between shrinking tiles

    void Start()
    {
        // Instantly set all tiles to scale 6x at the start
        foreach (GameObject tile in tiles)
        {
            tile.transform.localScale = new Vector3(6f, 6f, 1f);
        }

        // Start shrinking process from left to right
        StartCoroutine(ShrinkTilesLeftToRight());
    }

    // Shrink all tiles starting from the left
    IEnumerator ShrinkTilesLeftToRight()
    {
        // Sort tiles based on their X position (left to right)
        GameObject[] sortedTiles = SortTilesByXPosition();

        foreach (GameObject tile in sortedTiles)
        {
            StartCoroutine(ShrinkTile(tile));
            yield return new WaitForSeconds(delayBetweenShrinks);
        }
    }

    // Shrink a single tile down to 0
    IEnumerator ShrinkTile(GameObject tile)
    {
        Vector3 originalScale = tile.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < shrinkTime)
        {
            elapsedTime += Time.deltaTime;
            float scale = Mathf.Lerp(6f, 0f, elapsedTime / shrinkTime);
            tile.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        tile.SetActive(false);  // Optionally deactivate tile when done
    }

    // Helper function: Sort tiles by their X position (left to right)
    GameObject[] SortTilesByXPosition()
    {
        GameObject[] sorted = (GameObject[])tiles.Clone();
        System.Array.Sort(sorted, (tile1, tile2) => tile1.transform.position.x.CompareTo(tile2.transform.position.x));
        return sorted;
    }
}
