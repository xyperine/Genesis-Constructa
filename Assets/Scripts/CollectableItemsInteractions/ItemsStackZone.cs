using System.Collections;
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
        [SerializeField] private bool transferItems;

        private GridPlacement _placementArea;
        private IEnumerator _transferRoutineEnumerator;


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
            if (!transferItems)
            {
                return;
            }

            TryTransferTo(collector);
        }


        public virtual void TryTransferTo(Collector collector)
        {
            if (!CanTransferTo(collector))
            {
                return;
            }
            
            if (_transferRoutineEnumerator != null)
            {
                return;
            }
            
            _transferRoutineEnumerator = TransferRoutine(collector);
            StartCoroutine(_transferRoutineEnumerator);
        }


        private IEnumerator TransferRoutine(Collector collector)
        {
            StackZoneItem item = GetLast(collector.AcceptableResources);

            while (item)
            {
                if (NeedToBrakeTransferRoutine())
                {
                    _transferRoutineEnumerator = null;
                    yield break;
                }
                
                yield return new WaitForSeconds(0.2f);
                
                TryTransferItemTo(collector, item);
                item = GetLast(collector.AcceptableResources);
            }
            
            _transferRoutineEnumerator = null;
        }


        protected virtual bool NeedToBrakeTransferRoutine()
        {
            return false;
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
        

        public virtual void TryTransferItemTo(Collector collector, StackZoneItem item)
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