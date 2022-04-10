using System.Collections;
using System.Collections.Generic;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.Transfer
{
    public class TransferStackZoneBehaviour : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private StackZone stackZone;
        [SerializeField, Range(0.02f, 0.2f)] private float transferInterval = 0.1f;

        private readonly Dictionary<InteractionTarget, IEnumerator> _transferCoroutines =
            new Dictionary<InteractionTarget, IEnumerator>();


        public void TransferTo(InteractionTarget target)
        {
            if (!CanTransferTo(target))
            {
                return;
            }
            
            if (!_transferCoroutines.TryAdd(target, TransferItemsCoroutine(target)))
            {
                return;
            }

            StartCoroutine(_transferCoroutines[target]);
        }
        
        
        private bool CanTransferTo(InteractionTarget to)
        {
            bool hasItems = stackZone.HasItems;
            bool canTakeMore = to.CanTakeMore;

            return hasItems && canTakeMore;
        }
        
        
        private IEnumerator TransferItemsCoroutine(InteractionTarget target)
        {
            StackZoneItem item = stackZone.GetLast(target.AcceptableItems);

            while (item)
            {
                if (NeedToBrakeTransfer(target))
                {
                    break;
                }

                TransferSingleItemTo(target, item);

                yield return Helpers.GetWaitForSeconds(transferInterval);

                item = stackZone.GetLast(target.AcceptableItems);
            }

            _transferCoroutines.Remove(target);
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