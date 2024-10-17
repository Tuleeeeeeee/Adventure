using UnityEngine;

public class E14_MoveState : E_MoveState
{
    private Enemy14 enemy;
    private GameObject slime;
    private float cooldownTime = 0.2f;
    private float currentCooldown = 0f;
    public E14_MoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData, Enemy14 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleStates.SetFlipAfterIdle(true);
            stateManager.ChangeEnemyState(enemy.IdleStates);
        }
        // Check if the cooldown is active
        if (currentCooldown > 0)
        {
            // Reduce cooldown over time
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            createSlime();
            // Reset the cooldown timer
            currentCooldown = cooldownTime;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void createSlime()
    {
        slime = GameObject.Instantiate(enemy.Slime, enemy.transform.position + new Vector3(.9f, -.85f, 0), enemy.transform.rotation);
    }
}
