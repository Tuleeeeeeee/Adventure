public class E11_IdleState : E_IdleState
{
    private Enemy11 enemy;
    private bool isGrounded;
    public E11_IdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy11 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();
        Entity.Rb.gravityScale = 5;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isGrounded)
        {
            Entity.Rb.gravityScale = 1;
            if (isIdleTimeOver)
            {
                StateManager.ChangeEnemyState(enemy.MoveState);
            }
        }
    }
}
