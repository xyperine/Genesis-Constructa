using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.Transfer
{
    public abstract class SerializedTransferTarget : MonoBehaviour, ITransferTarget
    {
        public abstract bool CanTakeMore { get; }
        public abstract ResourceType[] AcceptableResources { get; }
        public abstract void Add(ZoneItem item);
    }
}