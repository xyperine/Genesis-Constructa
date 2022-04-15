using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.ItemsPlacement.Movers;
using MoonPioneerClone.ItemsPlacementsInteractions;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacement.Core.Area
{
    public abstract class PlacementArea : MonoBehaviour
    {
        [SerializeField] private PlacingPlacementItemsMover itemsMover;
        [SerializeField] protected PlacementAreaSettingsSO placementSettings;

        private PlacementAreaUpgradeableProperties _upgradeableProperties;
        
        protected IPlacementItemsCollection itemsCollection;
        protected PlacementItemPositionCalculator itemPositionCalculator;
        
        public int Count => itemsCollection.Count;
        public bool CanFitMore => Count < _upgradeableProperties.MaxItems;


        private void Awake()
        {
            _upgradeableProperties = new PlacementAreaUpgradeableProperties(placementSettings);
            
            InitializePositionCalculator();
            InitializeItemsCollection();
        }


        private void InitializePositionCalculator()
        {
            itemPositionCalculator = new PlacementItemPositionCalculator(placementSettings, _upgradeableProperties);
        }
        
        
        protected abstract void InitializeItemsCollection();


        public void Place(PlacementItem item)
        {
            if (!CanFitMore)
            {
                return;
            }
            
            MoveItemInside(item);
            itemsCollection.Add(item);
        }


        private void MoveItemInside(PlacementItem item)
        {
            Vector3 position = itemPositionCalculator.Calculate(itemsCollection.FirstNullIndex);
            itemsMover.MoveItem(item, position);
            item.Rotate(placementSettings.ItemRotation);
        }


        public virtual void Remove(PlacementItem item)
        {
            item.Discard();
            itemsCollection.Remove(item);
        }


        public PlacementItem GetLast(ItemType[] requiredItems)
        {
            IEnumerable<PlacementItem> placementItems = itemsCollection.Items;
            
            foreach (PlacementItem placementItem in placementItems)
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
        
        
        public void Upgrade(int newMaxItems)
        {
            itemsCollection.Upgrade(newMaxItems);
            _upgradeableProperties.Upgrade(newMaxItems);
        }


        public PlacementAreaUpgradeableProperties GetUpgradeableData()
        {
            return _upgradeableProperties ?? new PlacementAreaUpgradeableProperties(placementSettings);
        }
    }
}