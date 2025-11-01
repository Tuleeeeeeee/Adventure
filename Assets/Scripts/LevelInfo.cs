[System.Serializable]
public struct LevelInfo
{
    public int currentIndex;
    public int maxIndex;

    public LevelInfo(int current, int max)
    {
        currentIndex = current;
        maxIndex = max;
    }
}