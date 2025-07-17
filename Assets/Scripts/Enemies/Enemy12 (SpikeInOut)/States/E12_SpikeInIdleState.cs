using Tuleeeeee.CoreComponets;
using UnityEngine;

public class E12_SpikeInIdleState : E_IdleState
{
    private Enemy12 _enemy;
    public E12_SpikeInIdleState(Entity entity, StateManager stateManager, string animBoolName, D_IdleState stateData, Enemy12 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
      _enemy = enemy;
    }
    
    public override void Enter()
    {
        base.Enter();
        Core.GetComponentInChildren<Death>().enabled = true;
        _enemy.UpdateDamageableArea(new Vector2(1.4f, 0.6f), new Vector3(0, -0.5f, 0));

    }

    public override void Exit()
    {
        base.Exit();
        _enemy.UpdateDamageableArea(new Vector2(1.4f, 0.6f), new Vector3(0, 0, 0));
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleTimeOver)
        {
            StateManager.ChangeEnemyState(_enemy.SpikeOutIdleState);
        }
    }

}
