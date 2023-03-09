using System;
using ColonizationMobileGame.Items;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.ItemsRequirementsSystem;

namespace ColonizationMobileGame.ItemsPlacementsInteractions
{
    public class ItemsConsumer : InteractionTarget
    {
        private ItemsRequirementsChain _requirementsChain;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();

        public override bool CanTakeMore => _requirementsChain.NeedMore;
        public override ItemType[] AcceptableItems => _requirementsChain.RequiredItems;

        public event Action Consumed;


        public void Setup(ItemRequirementsBlock requirementsBlock)
        {
            Setup(new ItemsRequirementsChain(new[] {requirementsBlock}));
        }
        
        
        public void Setup(ItemsRequirementsChain requirementsChain)
        {
            _requirementsChain = requirementsChain;
        }


        public override void Add(StackZoneItem item)
        {
            item.SetFree();
            
            _requirementsChain.AddItem(item.Type);
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);
            
            Consumed?.Invoke();
        }
    }
}