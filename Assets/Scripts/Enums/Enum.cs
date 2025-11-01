
namespace Tuleeeeee.Enums
{
    public enum GameState
    {
        GameStart,
        Playing,
        LevelCompleted,
        Restart,
        Paused,
        GameOver,
        GameWon
    }
    public enum SwingType
    {
        Normal,
        Loop
    }

    #region PLATFORM_MOVEMENT
    public enum PlatformMovementType
    {
        Line,
        Circular,
        Zigzag
    };

    public enum LineMovementOrientation
    {
        Horizontal,
        Vertical
    }

    public enum CircularMovementOrientation
    {
        Clockwise,
        Counterclockwise
    }

    public enum PlatformTriggerType
    {
        Automatic,
        PlayerTrigger,
        SwitchTrigger
    }
    #endregion

    public enum CollectibleType
    {
        Fruit,
        Boost
    }
    public enum LevelDifficulty
    {
        Easy,
        Medium,
        Hard
    }
}
