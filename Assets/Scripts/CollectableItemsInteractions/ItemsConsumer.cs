using MoonPioneerClone.ResourceRequirementsSystem;
using MoonPioneerClone.WorldObjectsPlacement;
using MoonPioneerClone.WorldObjectsPlacement.OnePoint;
using UnityEngine;

namespace MoonPioneerClone.CollectableItemsInteractions
{
    [RequireComponent(typeof(OnePointPlacement))]
    public class ItemsConsumer : Collector, ICollectorInteractable
    {
        [SerializeField] private ResourceRequirementsChainSO requirementsChain;
        private OnePointPlacement _onePointPlacement;


        private void Awake()
        {
            GetComponents();
            UpdateAcceptedResources();
        }


        private void GetComponents()
        {
            _onePointPlacement = GetComponent<OnePointPlacement>();
        }


        private void UpdateAcceptedResources()
        {
            acceptedResources = requirementsChain.RequiredResources;
        }


        public override bool CanTakeMore()
        {
            return requirementsChain.NeedMore;
        }


        protected override void Add(StackZoneItem item)
        {
            if (!requirementsChain.NeedResource(item.Type))
            {
                return;
            }
            
            requirementsChain.AddResource(item.Type);
            _onePointPlacement.Add(item.GetComponent<WorldPlacementItem>());

            UpdateAcceptedResources();
        }


        public void Interact(Collector collector)
        {
            ItemsStackZone stackZone = collector as ItemsStackZone;

            if (!stackZone)
            {
                return;
            }
            
            stackZone.TryTransferTo(this);
        }
    }
}