namespace MoonPioneerClone.CollectableItemsInteractions.Split.Transfer
{
    public interface ITransferTarget
    {
        bool CanTakeMore { get; }
        ResourceType[] AcceptableResources { get; }

        void Add(ZoneItem item);
    }
}