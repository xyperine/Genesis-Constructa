using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.UI;
using ColonizationMobileGame.UnlockingSystem;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions
{
    public class ItemsConsumer : InteractionTarget
    {
        [SerializeField] private ItemsCountPanelData itemsCountPanelData;

        private IChain<IUnlockable> _chain;
        private ItemsRequirementsChain _requirementsChain;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();

        public override bool CanTakeMore => _requirementsChain.NeedMore;
        public override ItemType[] AcceptableItems => _requirementsChain.RequiredItems;


        public void Setup(ItemsRequirementsChain requirementsChain)
        {
            _requirementsChain = requirementsChain;
            
            SetStateObjectData();
        }


        public void Setup(IChain<IUnlockable> chain)
        {
            _chain = chain;
            _requirementsChain = _chain.RequirementsChain;
            
            SetStateObjectData();
        }
        

        public override void Add(StackZoneItem item)
        {
            item.SetFree();
            
            _requirementsChain.AddItem(item.Type);
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);

            SetStateObjectData();
        }


        private void SetStateObjectData()
        {
            itemsCountPanelData.SetData(_requirementsChain.CurrentBlock?.ToItemsCount(), _chain?.Current);
        }
    }
}