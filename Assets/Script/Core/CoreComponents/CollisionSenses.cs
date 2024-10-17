using UnityEngine;


public class CollisionSenses : CoreComponent
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private Movement movement;

    #region Check Transforms
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value;
    }
    /*public Transform LedgeCheckHorizontal
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }*/
    public Transform LedgeCheckVertical
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        private set => ledgeCheckVertical = value;
    }
    public Vector2 GroundCheckSize { get => groundCheckSize; set => groundCheckSize = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public float LedgeCheckDistance { get => ledgeCheckDistance; set => ledgeCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheckVertical;
    [SerializeField]
    private Vector2 groundCheckSize;
    [SerializeField]
    private float wallCheckDistance;
    [SerializeField]
    private float ledgeCheckDistance;

    [SerializeField]
    private LayerMask whatIsGround;

    #endregion

    /*   public bool Ceiling
       {
           get => Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, whatIsGround);
       }
   */
    public bool Ground
    {
        get => Physics2D.OverlapBox(GroundCheck.position, groundCheckSize, 0f, whatIsGround);
    }

    public bool WallFront
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    /*public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }*/

    public bool LedgeVertical
    {
        get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, ledgeCheckDistance, whatIsGround);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        if (GroundCheck)
        {
            Gizmos.color = Ground ? Color.green : Color.red;
            Gizmos.DrawCube(GroundCheck.position, groundCheckSize);
        }
        if (LedgeCheckVertical)
        {
            Gizmos.color = LedgeVertical ? Color.green : Color.red;
            Gizmos.DrawLine(LedgeCheckVertical.position, LedgeCheckVertical.position + (Vector3)(Vector2.down * ledgeCheckDistance));
        }
        if (WallCheck)
        {
            Gizmos.color = WallFront ? Color.green : Color.red;
            Gizmos.DrawLine(WallCheck.position, WallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * wallCheckDistance));

            Gizmos.color = WallBack ? Color.green : Color.red;
            Gizmos.DrawLine(WallCheck.position, WallCheck.position + (Vector3)(Vector2.right * -Movement.FacingDirection * wallCheckDistance));
        }

    }
}
