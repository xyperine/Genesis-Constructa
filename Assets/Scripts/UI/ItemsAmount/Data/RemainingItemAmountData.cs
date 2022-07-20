namespace ColonizationMobileGame.UI.ItemsAmount.Data
{
    public class RemainingItemAmountData : LimitedItemAmountData
    {
        public override string Presentation { get; }


        public RemainingItemAmountData(ItemType type, int current, int max) : base(type, current, max)
        {
            Presentation = $"<sprite={(int)type}> {max - current}";
        }
    }
}