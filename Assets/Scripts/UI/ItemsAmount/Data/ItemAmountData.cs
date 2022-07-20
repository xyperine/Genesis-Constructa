namespace ColonizationMobileGame.UI.ItemsAmount.Data
{
    public abstract class ItemAmountData
    {
        public ItemType Type { get; }
        public abstract string Presentation { get; }
        
        public virtual bool Visible => true;


        protected ItemAmountData(ItemType type)
        {
            Type = type;
        }
    }
}