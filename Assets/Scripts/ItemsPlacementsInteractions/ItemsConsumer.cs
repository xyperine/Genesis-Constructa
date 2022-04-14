using MoonPioneerClone.ItemsPlacement.Core;
using MoonPioneerClone.ItemsPlacement.Movers;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.ItemsRequirementsSystem;
using MoonPioneerClone.UpgradesSystem.Upgrading;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions
{
    public class ItemsConsumer : InteractionTarget
    {
        [SerializeField] private StackZoneUpgradesChainSO upgradesChain;
        private ItemsRequirementsChain _requirementsChain;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();

        public override bool CanTakeMore => _requirementsChain.NeedMore;
        public override ItemType[] AcceptableItems => _requirementsChain.RequiredItems;


        private void Start()
        {
            _requirementsChain = upgradesChain.RequirementsChain;
        }


        public override void Add(StackZoneItem item)
        {
            item.SetFree();
            
            _requirementsChain.AddItem(item.Type);
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);
        }
    }
}