using System;
using System.Collections;
using ColonizationMobileGame.Items;
using ColonizationMobileGame.ItemsPlacement.Core;
using ColonizationMobileGame.ItemsPlacement.Movers;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.ItemsRequirementsSystem;
using ColonizationMobileGame.Utility.Helpers;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions
{
    public class ItemsConsumer : InteractionTarget
    {
        [SerializeField, Min(0f)] private float delayBetweenPurchasesInSeconds = 0.5f;
        
        private readonly DestroyingPlacementItemsMover _itemsMover = new DestroyingPlacementItemsMover();
        
        private ItemsRequirementsChain _requirementsChain;
        private bool _onCooldown;

        public override bool CanTakeMore => !_onCooldown && _requirementsChain.NeedMore;
        public override ItemType[] AcceptableItems => _requirementsChain.RequiredItems;

        public event Action Consumed;


        public void Setup(ItemRequirementsBlock requirementsBlock)
        {
            Setup(new ItemsRequirementsChain(new[] {requirementsBlock}));
        }
        
        
        public void Setup(ItemsRequirementsChain requirementsChain)
        {
            _requirementsChain = requirementsChain;
            _requirementsChain.ChangingBlock += BeginWaitingForDelay;
        }


        private void Start()
        {
            BeginWaitingForDelay();
        }


        public override void Add(StackZoneItem item)
        {
            item.SetFree();
            
            _requirementsChain.AddItem(item.Type);
            _itemsMover.MoveItem(item.GetComponent<PlacementItem>(), transform.position);
            
            Consumed?.Invoke();
        }


        private void BeginWaitingForDelay()
        {
            StartCoroutine(WaitForDelayRoutine());
        }


        private IEnumerator WaitForDelayRoutine()
        {
            _onCooldown = true;
            
            yield return YieldInstructionsHelpers.GetWaitForSeconds(delayBetweenPurchasesInSeconds);

            _onCooldown = false;
        }


        private void OnDestroy()
        {
            if (_requirementsChain != null)
            {
                _requirementsChain.ChangingBlock -= BeginWaitingForDelay;
            }
        }
    }
}