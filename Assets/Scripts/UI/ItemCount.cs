namespace ColonizationMobileGame.UI
{
    public record ItemCount(ItemType Type, int Current, int Max = -1)
    {
        public ItemType Type { get; } = Type;
        public int Current { get; } = Current;
        public int Max { get; } = Max;

        public bool Filled { get; } = Max > 0 && Current == Max;
    }
}