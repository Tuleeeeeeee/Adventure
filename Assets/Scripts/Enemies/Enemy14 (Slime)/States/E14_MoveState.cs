using UnityEngine;

public class E14_MoveState : E_MoveState
{
    private Enemy14 enemy;
    private SlimeParticle slime;
    private float cooldownTime = 0.3f;
    private float currentCooldown = 0f;

    public E14_MoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData,
        Enemy14 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleStates.SetFlipAfterIdle(true);
            StateManager.ChangeEnemyState(enemy.IdleStates);
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

    private void createSlime()
    {
        slime = ObjectPool.DequeueObject<SlimeParticle>("slime");
        slime.gameObject.SetActive(true);
       // slime.transform.position = enemy.transform.position + new Vector3(1f * Movement.FacingDirection, -.85f, 0);
        slime.transform.position = enemy.spawnSlime.position;
    }
}