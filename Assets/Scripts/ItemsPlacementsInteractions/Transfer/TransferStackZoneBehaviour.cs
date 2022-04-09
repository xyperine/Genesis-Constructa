using System.Collections;
using System.Collections.Generic;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.Transfer
{
    public class TransferStackZoneBehaviour : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private StackZone stackZone;
        [SerializeField, Range(0.02f, 0.2f)] private float transferInterval = 0.1f;

        private WaitForSeconds _waitForTransferInterval;

        private readonly Dictionary<InteractionTarget, IEnumerator> _transferCoroutines =
            new Dictionary<InteractionTarget, IEnumerator>();


        private void OnValidate()
        {
            InitializeWaitForTransferInterval();
        }


        private void InitializeWaitForTransferInterval()
        {
            _waitForTransferInterval = new WaitForSeconds(transferInterval);
        }


        public void TransferTo(InteractionTarget target)
        {
            if (!_transferCoroutines.TryAdd(target, TransferItemsCoroutine(target)))
            {
                return;
            }

            StartCoroutine(_transferCoroutines[target]);
        }
        
        
        private IEnumerator TransferItemsCoroutine(InteractionTarget target)
        {
            StackZoneItem item = stackZone.GetLast(target.AcceptableItems);

            while (item)
            {
                if (NeedToBrakeTransfer())
                {
                    break;
                }

                if (!target.CanTakeMore)
                {
                    break;
                }

                TransferSingleItemTo(target, item);

                yield return _waitForTransferInterval;

                item = stackZone.GetLast(target.AcceptableItems);
            }

            _transferCoroutines.Remove(target);
        }


        protected virtual bool NeedToBrakeTransfer()
        {
            return false;
        }


        private void TransferSingleItemTo(InteractionTarget target, StackZoneItem item)
        {
            stackZone.Remove(item);
            target.Add(item);
        }
    }
}