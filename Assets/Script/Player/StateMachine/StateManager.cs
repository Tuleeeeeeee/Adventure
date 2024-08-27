public class StateManager
{
    public State CurrentState { get; private set; }

    public void Initialize(State stratingState)
    {
        CurrentState = stratingState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
