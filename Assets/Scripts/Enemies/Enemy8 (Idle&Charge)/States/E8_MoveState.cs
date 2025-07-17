using Tuleeeeee.Pathfinding;
using UnityEngine;

public class E8_MoveState : E_MoveState
{
    private Enemy8 enemy;


    public E8_MoveState(Entity entity, StateManager stateManager, string animBoolName, D_MoveState stateData,
        Enemy8 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        if (!isPlayerInMaxAgroRange)
        {
            StateManager.ChangeEnemyState(enemy.LookForPlayer);
        }

        SetTargetPosition(enemy.GetTargetPosition());
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        HandleMovement();
    }


    private void HandleMovement()
    {
        if (!isPlayerInMaxAgroRange)
        {
            // If the player is not visible, stop moving
            enemy.pathVectorList = null;
            StateManager.ChangeEnemyState(enemy.IdleState);
            return;
        }

        if (enemy.pathVectorList != null && enemy.pathVectorList.Count > 0)
        {
            // Get the current target position from the path list
            Vector3 targetPosition = enemy.pathVectorList[enemy.currentPathIndex];

            // Check if the object is more than 1 unit away from the target position
            if (Vector3.Distance(enemy.GetPosition(), targetPosition) > 1f)
            {
                // Calculate the direction to move towards the target position
                Vector3 moveDir = (targetPosition - enemy.GetPosition()).normalized;

                // Use the Movement class to set the velocity
                Movement.SetVelocity(10f, moveDir); // 10f is the speed
            }
            else
            {
                // If the object is close to the target position, move to the next target in the list
                enemy.currentPathIndex++;

                // Check if the object has reached the end of the path
                if (enemy.currentPathIndex >= enemy.pathVectorList.Count)
                {
                    // Stop moving by setting velocity to zero
                    StateManager.ChangeEnemyState(enemy.IdleState);
                }
            }
        }
        else
        {
            // If there is no path, stop moving
            StateManager.ChangeEnemyState(enemy.IdleState);
        }
    }

    private void SetTargetPosition(Vector3 position)
    {
        if (isPlayerInMaxAgroRange)
        {
            enemy.currentPathIndex = 0;
            enemy.pathVectorList = Pathfinding.instance.FindPath(enemy.GetPosition(), position);

            if (enemy.pathVectorList != null && enemy.pathVectorList.Count > 1)
            {
                enemy.pathVectorList.RemoveAt(0);
            }
        }
        else
        {
            // If the player is not visible, stop moving
            enemy.pathVectorList = null;
            StateManager.ChangeEnemyState(enemy.IdleState);
        }
    }
}