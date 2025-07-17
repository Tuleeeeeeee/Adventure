using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public Vector2 damageableAreaSize = new Vector2(1, 0.3f);

    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;


    public float closeRangeActionDistance = 1f;


    [Header("Layer Value")] public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}