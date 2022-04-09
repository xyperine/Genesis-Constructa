using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.Target
{
    public abstract class InteractionTarget : MonoBehaviour
    {
        public abstract bool CanTakeMore { get; }
        public abstract ItemType[] AcceptableItems { get; }

        public abstract void Add(StackZoneItem item);
    }
}