public class E1_DeadState : E_DeadState
{
    private Enemy1 enemy;
    public E1_DeadState(Entity entity, StateManager stateManager, string animBoolName, D_DeadState stateData, Enemy1 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}
