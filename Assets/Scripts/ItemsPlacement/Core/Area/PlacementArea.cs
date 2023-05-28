using System.Collections.Generic;
using System.Linq;
using GenesisConstructa.Items;
using GenesisConstructa.ItemsPlacement.Movers;
using GenesisConstructa.ItemsPlacementsInteractions;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacement.Core.Area
{
    public abstract class PlacementArea : MonoBehaviour
    { 
        [SerializeField] protected PlacementAreaSettingsSO placementSettings;
        [SerializeField] private bool mirror = true;
        
        private PlacementAreaUpgradeableProperties _upgradeableProperties;
        
        private PlacingPlacementItemsMover _itemsMover;

        protected IPlacementItemsCollection itemsCollection;
        protected PlacementItemPositionCalculator itemPositionCalculator;
        
        public int Count => itemsCollection?.Count ?? 0;
        public int Capacity => _upgradeableProperties.MaxItems;
        public bool CanFitMore => Count < _upgradeableProperties.MaxItems;
        public PlacementItem[] Items => itemsCollection.Items.ToArray();


        private void Awake()
        {
            _upgradeableProperties = new PlacementAreaUpgradeableProperties(placementSettings);
            _itemsMover = new PlacingPlacementItemsMover(transform);
            
            InitializePositionCalculator();
            InitializeItemsCollection();
        }


        private void InitializePositionCalculator()
        {
            itemPositionCalculator = new PlacementItemPositionCalculator(placementSettings, _upgradeableProperties, mirror);
        }
        
        
        protected abstract void InitializeItemsCollection();


        public void Setup(PlacementAreaSettingsSO settings)
        {
            placementSettings = settings;
        }


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
            MoveItem(item);
            RotateItem(item);
        }


        protected virtual void MoveItem(PlacementItem item)
        {
            Vector3 position = itemPositionCalculator.Calculate(itemsCollection.FirstNullIndex);
            _itemsMover.MoveItem(item, position);   
        }


        private void RotateItem(PlacementItem item)
        {
            switch (placementSettings.Alignment)
            {
                case PlacementAlignment.Origin:
                case PlacementAlignment.ForceHorizontal when item.Alignment == ItemAlignment.Horizontal:
                case PlacementAlignment.ForceVertical when item.Alignment == ItemAlignment.Vertical:
                    item.Rotate(Quaternion.identity);
                    return;
                
                default:
                    item.Rotate(placementSettings.ItemRotation);
                    return;
            }
        }


        public virtual void Remove(PlacementItem item)
        {
            item.Discard();
            itemsCollection.Remove(item);
        }


        public PlacementItem GetLast(ItemType[] requiredItems)
        {
            IEnumerable<PlacementItem> placementItems = itemsCollection.Items.Reverse();
            
            foreach (PlacementItem placementItem in placementItems)
            {
                if (!placementItem)
                {
                    continue;
                }

                if (placementItem.Moving)
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