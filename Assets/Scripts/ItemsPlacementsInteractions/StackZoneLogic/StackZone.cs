using System.Linq;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Core.Area;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.UpgradingSystem;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic
{
    public class StackZone : InteractionTarget, IUpgradeable<StackZoneUpgradeData>
    {
        [SerializeField] private ItemType[] acceptableItems; 
        
        private PlacementArea _placement;
        
        public bool HasItems => _placement.Count > 0;
        public override bool CanTakeMore => _placement.CanFitMore;
        public override ItemType[] AcceptableItems => (ItemType[]) acceptableItems.Clone();


        private void Awake()
        {
            GetComponents();
        }


        private void OnValidate()
        {
            if (!TryGetComponent(out _placement))
            {
                Debug.LogError("No PlacementArea component attached!");
            }
        }


        private void GetComponents()
        {
            _placement = GetComponent<PlacementArea>();
        }


        public void Setup(ItemType[] acceptableItems)
        {
            this.acceptableItems = acceptableItems;
        }


        public override void Add(StackZoneItem item)
        {
            if (!CanTakeThisItem(item))
            {
                return;
            }

            item.SetFree();
            item.SetZone(this);
            
            _placement.Place(item.GetComponent<PlacementItem>());
        }
        
        
        private bool CanTakeThisItem(StackZoneItem item)
        {
            return acceptableItems.Contains(item.Type) && CanTakeMore;
        }


        public void Remove(StackZoneItem item)
        {
            item.SetZone(null);
            _placement.Remove(item.GetComponent<PlacementItem>());
        }


        public StackZoneItem GetLast(ItemType[] requestedItems)
        {
            if (requestedItems == null)
            {
                return null;
            }
            
            PlacementItem placementItem = _placement.GetLast(requestedItems);

            if (!placementItem)
            {
                return null;
            }
            
            StackZoneItem item;
            placementItem.TryGetComponent(out item);

            return item;
        }


        public void Upgrade(StackZoneUpgradeData data)
        {
            _placement.Upgrade(data.Capacity);
        }
    }
}