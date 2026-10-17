using Tuleeeeee.Utils;
using UnityEngine;

public class E15_ChargeState : E_ChargeState
{
    private Enemy15 enemy;

    public E15_ChargeState(Entity entity, StateManager stateManager, string animBoolName, D_ChargeState stateData,
        Enemy15 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isDetectingWall)
        {
            enemy.Animator.SetTrigger("wall");
         //   Utils.ShakeCamera(.1f, .3f);
            StateManager.ChangeEnemyState(enemy.LookForPlayerState);
        }
    }
}