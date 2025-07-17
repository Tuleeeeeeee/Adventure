public class StateManager
{
    public PlayerState CurrentPlayerState { get; private set; }

    public void Initialize(PlayerState stratingState)
    {
        CurrentPlayerState = stratingState;
        CurrentPlayerState.Enter();
    }
    public void ChangeState(PlayerState newState)
    {
        CurrentPlayerState.Exit();
        CurrentPlayerState = newState;
        CurrentPlayerState.Enter();
    }
    public EnemiesState CurrentEnemyState { get; private set; }

    public void InitializeEnemy(EnemiesState startingState)
    {
        CurrentEnemyState = startingState;
        CurrentEnemyState.Enter();
    }

    public void ChangeEnemyState(EnemiesState newState)
    {
        CurrentEnemyState.Exit();
        CurrentEnemyState = newState;
        CurrentEnemyState.Enter();
    }
}
