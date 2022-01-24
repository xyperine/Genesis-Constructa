using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.ItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public abstract class WorldPlacementArea<TPlacementSettings> : MonoBehaviour
        where TPlacementSettings : WorldPlacementSettingsSO
    {
        [SerializeField] protected TPlacementSettings placementSettings;

        protected IWorldPlacementItemsKeepingBehaviour itemsKeepingBehaviour;
        
        public int Count => itemsKeepingBehaviour.Count;
        public abstract bool CanFitMore { get; }


        private void Awake()
        {
            InitializeItemsKeepingBehaviour();
        }


        protected abstract void InitializeItemsKeepingBehaviour();


        public void Add(WorldPlacementItem item)
        {
            if (!CanFitMore)
            {
                return;
            }
            
            MoveItemInside(item);

            itemsKeepingBehaviour.TryAdd(item);
        }


        protected abstract void MoveItemInside(WorldPlacementItem item);


        protected abstract Vector3 GetPositionForNewItem();
        

        public void Remove(WorldPlacementItem item)
        {
            item.Discard();
            itemsKeepingBehaviour.TryRemove(item);
        }


        public WorldPlacementItem GetLast()
        {
            return itemsKeepingBehaviour.TryPeek();
        }


        public WorldPlacementItem GetLast(ResourceType[] acceptableResources)
        {
            IEnumerable<WorldPlacementItem> placementItems = itemsKeepingBehaviour.Items;
            
            foreach (WorldPlacementItem placementItem in placementItems)
            {
                if (!placementItem)
                {
                    continue;
                }
                
                ZoneItem item;
                if (!placementItem.TryGetComponent(out item))
                {
                    continue;
                }

                if (acceptableResources.Contains(item.Type))
                {
                    return placementItem;
                }
            }

            return null;
        }
    }
}