using System;

namespace GenesisConstructa.Timer
{
    // Thresholds in seconds
    public enum GameTimerPhase
    {
        Normal = int.MaxValue,
        Dangerous = 3 * 60,
        Critical = 10,
        Over = 0,
    }
}