using MoonPioneerClone.ItemsPlacement.Core;
using MoonPioneerClone.ItemsPlacement.Movers;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.ItemsRequirementsSystem;

namespace MoonPioneerClone.ItemsPlacementsInteractions
{
    public class ItemsConsumer : InteractionTarget
    {
        private ItemsRequirementsChain _requirementsChain;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();

        public override bool CanTakeMore => _requirementsChain.NeedMore;
        public override ItemType[] AcceptableItems => _requirementsChain.RequiredItems;


        public void Setup(ItemsRequirementsChain requirementsChain)
        {
            _requirementsChain = requirementsChain;
        }


        public override void Add(StackZoneItem item)
        {
            item.SetFree();
            
            _requirementsChain.AddItem(item.Type);
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);
        }
    }
}