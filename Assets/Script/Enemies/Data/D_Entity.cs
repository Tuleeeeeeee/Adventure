using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    [Header("Health Value")]
    public float maxHealth = 30f;

    [Header("Damage Value")]
    public float damageHopSpeed = 3f;

    [Header("Check Value")]
/*    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;*/

    public Vector2 damageableAreaSize = new Vector2(1,0.3f);

    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;


    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    public float closeRangeActionDistance = 1f;

    [Header("Particle")]
    public GameObject hitParticle;

    [Header("Layer Value")]
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
