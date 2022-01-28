using System.Linq;
using MoonPioneerClone.ItemsInteractions.Transfer.Target;
using MoonPioneerClone.WorldObjectsPlacement;
using MoonPioneerClone.WorldObjectsPlacement.Placements.Grid;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    [RequireComponent(typeof(GridPlacement))]
    public class StackZone : TransferTarget
    {
        [SerializeField] private ItemType[] acceptableItems; 
        
        private GridPlacement _placement;
        
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
            _placement = GetComponent<GridPlacement>();
        }


        public override void Add(StackZoneItem item)
        {
            item.SetZone(this);
            _placement.Add(item.GetComponent<WorldPlacementItem>());
        }


        public void Remove(StackZoneItem item)
        {
            item.SetZone(null);
            _placement.Remove(item.GetComponent<WorldPlacementItem>());
        }


        public StackZoneItem GetLast(ItemType[] requestedItems)
        {
            WorldPlacementItem placementItem = _placement.GetLast(requestedItems);

            if (!placementItem)
            {
                return null;
            }
            
            StackZoneItem item;
            placementItem.TryGetComponent(out item);

            return item;
        }
    }
}