using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileData", menuName = "Data/Tile Data/Base Data")]
public class TileData : ScriptableObject
{
    public TileBase[] Tiles;

    public float movementVelocity = 5f;

}
