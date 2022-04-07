using System.Linq;
using MoonPioneerClone.ItemsInteractions.Transfer.Target;
using MoonPioneerClone.ItemsPlacement;
using MoonPioneerClone.ItemsPlacement.Core;
using MoonPioneerClone.ItemsPlacement.Core.Area;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.StackZoneLogic
{
    [RequireComponent(typeof(PlacementArea))]
    public class StackZone : TransferTarget
    {
        [SerializeField] private ItemType[] acceptableItems; 
        
        private PlacementArea _placement;
        
        public bool HasItems => _placement.Count > 0;
        public override bool CanTakeMore => _placement.CanFitMore;
        public override ItemType[] AcceptableItems => (ItemType[]) acceptableItems.Clone();

        public bool CanTakeThisItem(ItemType type) => acceptableItems.Contains(type);


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
            item.SetZone(this);
            _placement.Place(item.GetComponent<PlacementItem>());
        }


        public void Remove(StackZoneItem item)
        {
            item.SetZone(null);
            _placement.Remove(item.GetComponent<PlacementItem>());
        }


        public StackZoneItem GetLast(ItemType[] requestedItems)
        {
            PlacementItem placementItem = _placement.GetLast(requestedItems);

            if (!placementItem)
            {
                return null;
            }
            
            StackZoneItem item;
            placementItem.TryGetComponent(out item);

            return item;
        }


        public void Upgrade(int newMaxItems)
        {
            _placement.Upgrade(newMaxItems);
        }
    }
}