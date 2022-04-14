using System.Linq;
using MoonPioneerClone.ItemsPlacement.Core;
using MoonPioneerClone.ItemsPlacement.Core.Area;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.UpgradingSystem;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic
{
    [RequireComponent(typeof(PlacementArea))]
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


        private void GetComponents()
        {
            _placement = GetComponent<PlacementArea>();
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