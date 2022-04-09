using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.Transfer.Target
{
    public abstract class TransferTarget : MonoBehaviour
    {
        public abstract bool CanTakeMore { get; }
        public abstract ItemType[] AcceptableItems { get; }

        public abstract void Add(StackZoneItem item);
    }
}