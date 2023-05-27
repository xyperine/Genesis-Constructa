using System;

namespace GenesisConstructa.GameFading
{
    [Flags]
    public enum FadeFlags
    {
        Time = 1 << 0,
        Audio = 1 << 1,
        Visuals = 1 << 2,
        All = ~(~0 << 3),
    }
}