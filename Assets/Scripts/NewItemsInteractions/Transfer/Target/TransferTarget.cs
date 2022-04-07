using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.Transfer.Target
{
    public abstract class TransferTarget : MonoBehaviour
    {
        public abstract bool CanTakeMore { get; }
        public abstract ItemType[] AcceptableItems { get; }

        public abstract void Add(StackZoneItem item);
    }
}