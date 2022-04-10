using System;
using MoonPioneerClone.ItemsPlacement.Core;
using MoonPioneerClone.ItemsPlacement.Movers;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.ItemsRequirementsSystem;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions
{
    public class ItemsConsumer : InteractionTarget
    {
        [SerializeField] private ItemsRequirementsChainSO requirementsChain;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();

        public override bool CanTakeMore => requirementsChain.NeedMore;
        public override ItemType[] AcceptableItems => requirementsChain.RequiredItems;

        public event Action BlockSatisfied;


        private void Awake()
        {
            requirementsChain.BlockSatisfied += InvokeBlockSatisfied;
        }


        private void InvokeBlockSatisfied()
        {
            BlockSatisfied?.Invoke();
        }


        public override void Add(StackZoneItem item)
        {
            item.SetFree();
            
            requirementsChain.AddItem(item.Type);
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);
        }
    }
}