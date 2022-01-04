using System.Linq;
using UnityEngine;

namespace MoonPioneerClone.CollectableItemsInteractions
{
    public abstract class Collector : MonoBehaviour
    {
        [SerializeField] protected ResourceType[] acceptedResources;

        public ResourceType[] AcceptedResources => (ResourceType[]) acceptedResources?.Clone();
        public bool CanTakeThisResource(ResourceType type) => acceptedResources.Contains(type);
        
        
        public virtual void TryAdd(StackZoneItem item)
        {
            if (!CanTakeMore())
            {
                return;
            }
            
            Add(item);
        }
        
        
        public abstract bool CanTakeMore();
        protected abstract void Add(StackZoneItem item);
    }
}