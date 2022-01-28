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

        protected IWorldPlacementItemsCollection itemsCollection;
        
        public int Count => itemsCollection.Count;
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

            itemsCollection.Add(item);
        }


        protected virtual void MoveItemInside(WorldPlacementItem item)
        {
            Vector3 position = CalculatePositionForNewItem();

            item.transform.SetParent(transform);
            item.MoveToArea(position);
        }


        protected abstract Vector3 CalculatePositionForNewItem();
        

        public virtual void Remove(WorldPlacementItem item)
        {
            item.Discard();
            itemsCollection.Remove(item);
        }


        public WorldPlacementItem GetLast()
        {
            return itemsCollection.Peek();
        }


        public WorldPlacementItem GetLast(ItemType[] requiredItems)
        {
            IEnumerable<WorldPlacementItem> placementItems = itemsCollection.Items;
            
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

                if (requiredItems.Contains(item.Type))
                {
                    return placementItem;
                }
            }

            return null;
        }
    }
}