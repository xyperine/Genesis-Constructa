using System;
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
        private GridPlacement _gridPlacementArea;


        private void Awake()
        {
            GetGridPlacement();
        }


        private void GetGridPlacement()
        {
            _gridPlacementArea = GetComponent<GridPlacement>();
        }


        public void TryTransferTo(Collector collector)
        {
            if (!collector)
            {
                throw new ArgumentNullException(nameof(collector));
            }
            
            StackZoneItem item = GetLast(collector.AcceptedResources);
            if (!item)
            {
                return;
            }
    
            TryTransferItemTo(item, collector);
        }


        private StackZoneItem GetLast(ResourceType[] acceptableResources)
        {
            WorldPlacementItem placementItem = _gridPlacementArea.GetLast(acceptableResources);

            if (!placementItem)
            {
                return null;
            }
            
            StackZoneItem item;
            placementItem.TryGetComponent(out item);
            return item;
        }
        

        public void TryTransferItemTo(StackZoneItem item, Collector collector)
        {
            if (!item)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!collector)
            {
                throw new ArgumentNullException(nameof(collector));
            }
            
            if (!CanTransferTo(collector))
            {
                return;
            }

            if (!collector.CanTakeThisResource(item.Type))
            {
                return;
            }

            _gridPlacementArea.Remove(item.GetComponent<WorldPlacementItem>());
            collector.TryAdd(item);
        }
        
        
        private bool CanTransferTo(Collector to)
        {
            if (!to)
            {
                throw new ArgumentNullException(nameof(to));
            }
            
            ItemsStackZone from = this;

            bool hasItems = from._gridPlacementArea.Count > 0;
            bool canGiveTo = from.giveTo.Contains(to);
            bool toCanTake = to.CanTakeMore();
            bool notTheSameEntity = to != from;

            return hasItems && canGiveTo && toCanTake && notTheSameEntity;
        }


        public override bool CanTakeMore()
        {
            return _gridPlacementArea.CanFitMore;
        }


        protected override void Add(StackZoneItem item)
        {
            if (!item)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!acceptedResources.Contains(item.Type))
            {
                return;
            }
            
            item.SetZone(this);
            _gridPlacementArea.Add(item.GetComponent<WorldPlacementItem>());
        }


        public void Interact(Collector collector)
        {
            if (!collector)
            {
                throw new ArgumentNullException(nameof(collector));
            }
            
            TryTransferTo(collector);
        }
    }
}