public class E5_DeadState : E_DeadState
{
    private Enemy5 enemy;

    public E5_DeadState(Entity entity, StateManager stateManager, string animBoolName, D_DeadState stateData, Enemy5 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}
