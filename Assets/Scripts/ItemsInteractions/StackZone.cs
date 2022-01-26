using System.Linq;
using MoonPioneerClone.ItemsInteractions.Transfer;
using MoonPioneerClone.ItemsInteractions.Transfer.Target;
using MoonPioneerClone.WorldObjectsPlacement;
using MoonPioneerClone.WorldObjectsPlacement.Grid;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    [RequireComponent(typeof(GridPlacement))]
    public class StackZone : TransferTarget
    {
        [SerializeField] private ResourceType[] acceptableResources; 
        
        private GridPlacement _placement;
        
        public bool HasItems => _placement.Count > 0;
        public override bool CanTakeMore => _placement.CanFitMore;
        public bool CanTakeThisResource(ResourceType type) => acceptableResources.Contains(type);
        public override ResourceType[] AcceptableResources => (ResourceType[]) acceptableResources.Clone();


        private void Awake()
        {
            GetComponents();
        }


        private void GetComponents()
        {
            _placement = GetComponent<GridPlacement>();
        }


        public override void Add(ZoneItem item)
        {
            item.SetZone(this);
            _placement.Add(item.GetComponent<WorldPlacementItem>());
        }


        public void Remove(ZoneItem item)
        {
            item.SetZone(null);
            _placement.Remove(item.GetComponent<WorldPlacementItem>());
        }


        public ZoneItem GetLast(ResourceType[] resources)
        {
            WorldPlacementItem placementItem = _placement.GetLast(resources);

            if (!placementItem)
            {
                return null;
            }
            
            ZoneItem item;
            placementItem.TryGetComponent(out item);

            return item;
        }
    }
}