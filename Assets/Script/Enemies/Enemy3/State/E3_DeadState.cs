
public class E3_DeadState : E_DeadState
{
    private Enemy3 enemy;
    public E3_DeadState(Entity entity, StateManager stateManager, string animBoolName, D_DeadState stateData, Enemy3 enemy) : base(entity, stateManager, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

}
