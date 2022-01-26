using System.Collections;
using System.Collections.Generic;
using MoonPioneerClone.ItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.Transfer
{
    public class TransferStackZoneBehavior : MonoBehaviour
    {
        [SerializeField] private StackZone stackZone;
        [SerializeField] private List<TransferTarget> giveTo = new List<TransferTarget>();

        private readonly WaitForSeconds transferInterval = new WaitForSeconds(0.2f);

        private readonly Dictionary<TransferTarget, IEnumerator> _transferRoutines =
            new Dictionary<TransferTarget, IEnumerator>();

        public bool CanGiveTo(TransferTarget to) => giveTo.Contains(to as TransferTarget);


        public void TransferTo(TransferTarget target)
        {
            if (!_transferRoutines.TryAdd(target, TransferItemsRoutine(target)))
            {
                return;
            }
            
            StartCoroutine(_transferRoutines[target]);
        }
        
        
        private IEnumerator TransferItemsRoutine(TransferTarget target)
        {
            ZoneItem item = stackZone.GetLast(target.AcceptableResources);

            while (item)
            {
                if (NeedToBrakeTransferRoutine())
                {
                    break;
                }

                if (!target.CanTakeMore)
                {
                    break;
                }

                TransferSingleItemTo(target, item);

                yield return transferInterval;

                item = stackZone.GetLast(target.AcceptableResources);
            }

            _transferRoutines.Remove(target);
        }


        protected virtual bool NeedToBrakeTransferRoutine()
        {
            return false;
        }


        private void TransferSingleItemTo(TransferTarget target, ZoneItem item)
        {
            stackZone.Remove(item);
            target.Add(item);
        }
    }
}