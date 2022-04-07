using System;
using MoonPioneerClone.ItemsPlacement.Core;
using MoonPioneerClone.ItemsPlacement.Movers;
using MoonPioneerClone.ItemsRequirementsSystem;
using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions
{
    public class ItemsConsumer : TransferTarget
    {
        [SerializeField] private ItemsRequirementsChainSO requirementsChain;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();

        public override bool CanTakeMore => requirementsChain.NeedMore;
        public override ItemType[] AcceptableItems => requirementsChain.RequiredItems;

        public event Action BlockSatisfied;


        private void Awake()
        {
            GetComponents();

            requirementsChain.BlockSatisfied += InvokeBlockSatisfied;
        }


        private void InvokeBlockSatisfied()
        {
            BlockSatisfied?.Invoke();
        }


        private void GetComponents()
        {

        }
        
        
        public override void Add(StackZoneItem item)
        {
            requirementsChain.AddItem(item.Type);
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);
        }
    }
}