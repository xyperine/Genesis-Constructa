using MoonPioneerClone.ItemsInteractions.Transfer.Target;
using MoonPioneerClone.ItemsRequirementsSystem;
using MoonPioneerClone.WorldObjectsPlacement;
using MoonPioneerClone.WorldObjectsPlacement.Placements.Null;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    [RequireComponent(typeof(NullPlacement))]
    public class ItemsConsumer : TransferTarget
    {
        [SerializeField] private ItemsRequirementsChainSO requirementsChain;
        
        private NullPlacement _placement;

        public override bool CanTakeMore => requirementsChain.NeedMore && _placement.CanFitMore;
        public override ItemType[] AcceptableItems => requirementsChain.RequiredItems;


        private void Awake()
        {
            GetComponents();
        }


        private void GetComponents()
        {
            _placement = GetComponent<NullPlacement>();
        }
        
        
        public override void Add(StackZoneItem item)
        {
            requirementsChain.AddItem(item.Type);
            _placement.Add(item.GetComponent<WorldPlacementItem>());
        }
    }
}