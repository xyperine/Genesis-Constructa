using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.WorldObjectsPlacement;
using MoonPioneerClone.WorldObjectsPlacement.Grid;
using UnityEngine;

namespace MoonPioneerClone.CollectableItemsInteractions
{
    [RequireComponent(typeof(GridPlacement))]
    public class ItemsStackZone : Collector, ICollectorInteractable
    {
        [SerializeField] private List<Collector> giveTo = new List<Collector>();
        [SerializeField] private bool transferItemsOnTouch;

        private GridPlacement _placementArea;


        private void Awake()
        {
            GetComponents();
        }


        private void GetComponents()
        {
            _placementArea = GetComponent<GridPlacement>();
        }


        public void Interact(Collector collector)
        {
            if (!transferItemsOnTouch)
            {
                return;
            }

            TryTransferTo(collector);
        }
        
        
        public void TryTransferTo(Collector collector)
        {
            StackZoneItem item = GetLast(collector.AcceptableResources);
            if (!item)
            {
                return;
            }
    
            TryTransferItemTo(collector, item);
        }


        private StackZoneItem GetLast(ResourceType[] acceptableResources)
        {
            WorldPlacementItem placementItem = _placementArea.GetLast(acceptableResources);

            if (!placementItem)
            {
                return null;
            }
            
            StackZoneItem item;
            placementItem.TryGetComponent(out item);
            
            return item;
        }
        

        public void TryTransferItemTo( Collector collector, StackZoneItem item)
        {
            if (!CanTransferTo(collector))
            {
                return;
            }

            if (!collector.CanTakeThisResource(item.Type))
            {
                return;
            }

            _placementArea.Remove(item.GetComponent<WorldPlacementItem>());
            collector.TryAdd(item);
        }
        
        
        private bool CanTransferTo(Collector to)
        { 
            ItemsStackZone from = this;

            bool hasItems = from._placementArea.Count > 0;
            bool canGiveTo = from.giveTo.Contains(to);
            bool toCanTake = to.CanTakeMore();
            bool notTheSameEntity = to != from;

            return hasItems && canGiveTo && toCanTake && notTheSameEntity;
        }


        public override bool CanTakeMore()
        {
            return _placementArea.CanFitMore;
        }


        protected override void Add(StackZoneItem item)
        {
            if (!acceptedResources.Contains(item.Type))
            {
                return;
            }
            
            item.SetZone(this);
            _placementArea.Add(item.GetComponent<WorldPlacementItem>());
        }
    }
}