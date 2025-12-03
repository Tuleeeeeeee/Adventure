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
    public LayerMask whatIsEnemy;

    [Header("Wall slide state")]
    public float wallSlideVelocity = 3f;

    [Header("Wall slide state")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Tooltip("A constant downward force applied while grounded. Helps on slopes"), Range(0f, -10f)]
    public float groundingForce = -1.5f;

    [Tooltip("The player's capacity to gain horizontal speed")]
    public float acceleration = 120;

    [Tooltip("The pace at which the player comes to a stop")]
    public float groundDeceleration = 60;

    [Tooltip("Deceleration in air only after stopping input mid-air")]
    public float airDeceleration = 30;

    [Tooltip("The maximum vertical movement speed")]
    public float maxFallSpeed = 40;

    [Tooltip("The player's capacity to gain fall speed. a.k.a. In Air Gravity")]
    public float fallAcceleration = 110;

    [Tooltip("The gravity multiplier added when jump is released early")]
    public float jumpEndEarlyGravityModifier = 3;

}
