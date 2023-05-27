namespace GenesisConstructa.Timer
{
    // Thresholds in seconds
    public enum GameTimerPhase
    {
        Normal = 20 * 60,
        Dangerous = 3 * 60,
        Critical = 10,
        Over = 0,
    }
}