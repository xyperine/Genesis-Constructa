using System;
using System.Linq;
using MoonPioneerClone.CollectableItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    // Do something with a KeepItems mess
    public abstract class WorldPlacementArea<TPlacementSettings> : MonoBehaviour
        where TPlacementSettings : WorldPlacementSettingsSO
    {
        [SerializeField] protected TPlacementSettings placementSettings;

        protected WorldPlacementItemsCollection items;
        
        public int Count => items.Count;
        public bool CanFitMore => Count < placementSettings.MaxItems || !placementSettings.KeepItems;
        
        
        private void Awake()
        {
            InitializeItemsCollection();
        }


        private void InitializeItemsCollection()
        {
            items = new WorldPlacementItemsCollection(placementSettings.MaxItems);
        }


        public void Add(WorldPlacementItem item)
        {
            if (!CanFitMore)
            {
                return;
            }
            
            Vector3 position = GetPositionForNewItem();
            MoveItem(item, position);
            
            if (!placementSettings.KeepItems)
            {
                return;
            }
            
            items.Add(item);
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

            if (!placementSettings.KeepItems)
            {
                return;
            }
            
            items.Remove(item);
        }


        public WorldPlacementItem GetLast()
        {
            if (!placementSettings.KeepItems)
            {
                throw new OperationCanceledException("Cannot keep items!");
            }
            
            print(items.Peek());
            return items.Peek();
        }


        public WorldPlacementItem GetLast(ResourceType[] acceptableResources)
        {

            WorldPlacementItem[] placementItems = items.Items;
            
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