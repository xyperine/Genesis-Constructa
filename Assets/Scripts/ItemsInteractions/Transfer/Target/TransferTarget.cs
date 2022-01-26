using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.Transfer.Target
{
    public abstract class TransferTarget : MonoBehaviour
    {
        public abstract bool CanTakeMore { get; }
        public abstract ResourceType[] AcceptableResources { get; }

        public abstract void Add(ZoneItem item);
    }
}