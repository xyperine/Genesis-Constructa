namespace ColonizationMobileGame.UI.ItemsAmount.Data
{
    public class InItemAmountData : ItemAmountData
    {
        public override string Presentation { get; }


        public InItemAmountData(ItemType type, int current) : base(type)
        {
            Presentation = $"<sprite={(int)type}> {current}";
        }
    }
}