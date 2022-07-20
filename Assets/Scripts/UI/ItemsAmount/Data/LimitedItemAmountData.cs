namespace ColonizationMobileGame.UI.ItemsAmount.Data
{
    public class LimitedItemAmountData : ItemAmountData
    {
        public override string Presentation { get; }
        public override bool Visible { get; }
        

        public LimitedItemAmountData(ItemType type, int current, int max) : base(type)
        {
            Presentation = $"<sprite={(int)Type}> {current}/{max}";
            Visible = current != max;
        }
    }
}