using MoonPioneerClone.ItemsInteractions.Transfer;
using MoonPioneerClone.ResourceRequirementsSystem;
using MoonPioneerClone.WorldObjectsPlacement;
using MoonPioneerClone.WorldObjectsPlacement.OnePoint;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    [RequireComponent(typeof(OnePointPlacement))]
    public class Consumer : SerializedTransferTarget
    {
        [SerializeField] private ResourceRequirementsChainSO requirementsChain;
        
        private OnePointPlacement _placement;

        public override bool CanTakeMore => requirementsChain.NeedMore && _placement.CanFitMore;
        public override ResourceType[] AcceptableResources => requirementsChain.RequiredResources;


        private void Awake()
        {
            GetComponents();
        }


        private void GetComponents()
        {
            _placement = GetComponent<OnePointPlacement>();
        }
        
        
        public override void Add(ZoneItem item)
        {
            requirementsChain.AddResource(item.Type);
            _placement.Add(item.GetComponent<WorldPlacementItem>());
        }
    }
}