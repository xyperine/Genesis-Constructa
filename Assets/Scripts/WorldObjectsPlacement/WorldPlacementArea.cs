using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.CollectableItemsInteractions;
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
            
            Vector3 position = GetPositionForNewItem();
            MoveItem(item, position);

            itemsKeepingBehaviour.TryAdd(item);
        }


        protected abstract Vector3 GetPositionForNewItem();


        private void MoveItem(WorldPlacementItem item, Vector3 position)
        {
            item.transform.SetParent(transform);
            item.Rotate();
            item.Move(position);
        }


        public void Remove(WorldPlacementItem item)
        {
            item.transform.SetParent(null);
            item.Rotate();

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
                
                StackZoneItem item;
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