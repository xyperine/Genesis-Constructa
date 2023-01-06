using System.Collections;
using System.Collections.Generic;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.Utility.Helpers;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.Transfer
{
    public class TransferStackZoneBehaviour : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private StackZone stackZone;
        [SerializeField, Range(0.02f, 0.2f)] private float transferInterval = 0.1f;

        private readonly List<InteractionTarget> _activeTransferTargets = new List<InteractionTarget>();

        public bool CanGive => stackZone.HasItems;


        public void Setup(StackZone zone)
        {
            stackZone = zone;
        }
        

        public void TransferTo(InteractionTarget target)
        {
            if (!CanTransferTo(target))
            {
                return;
            }
            
            if (_activeTransferTargets.Contains(target))
            {
                return;
            }

            StartCoroutine(TransferItemsCoroutine(target));
        }
        
        
        private bool CanTransferTo(InteractionTarget to)
        {
            bool hasItems = stackZone.HasItems;
            bool canTakeMore = to.CanTakeMore;

            return hasItems && canTakeMore;
        }
        
        
        private IEnumerator TransferItemsCoroutine(InteractionTarget target)
        {
            _activeTransferTargets.Add(target);
            
            StackZoneItem item = stackZone.GetLast(target.AcceptableItems);
            
            while (item)
            {
                if (NeedToBrakeTransfer(target))
                {
                    break;
                }

                TransferSingleItemTo(target, item);

                yield return YieldInstructionsHelpers.GetWaitForSeconds(transferInterval);

                item = stackZone.GetLast(target.AcceptableItems);
            }

            _activeTransferTargets.Remove(target);
        }


        protected virtual bool NeedToBrakeTransfer(InteractionTarget target)
        {
            return !CanTransferTo(target);
        }


        private void TransferSingleItemTo(InteractionTarget target, StackZoneItem item)
        {
            target.Add(item);
        }
    }
}