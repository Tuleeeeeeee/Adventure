using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player Data/base Data")]
public class PlayerData : ScriptableObject
{

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public int amountOfJumps = 1;
    public float jumpHeight = 25f;
    
    [Header("Gravity")]
    public float gravityScale = 3f;

    [Header("Coyote")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Check variables")]
    public float platformCheckDistance = 2f;
    public LayerMask whatIsPlatfrom;
    public LayerMask whatIsEnemy;

    [Header("Wall slide state")]
    public float wallSlideVelocity = 3f;

    [Header("Wall slide state")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);


#if UNITY_EDITOR
    void OnValidate()
    {

    }
#endif
}
