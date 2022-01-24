using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.Transfer
{
    public class TransferStackZoneBehavior : MonoBehaviour
    {
        [SerializeField] private StackZone stackZone;
        [SerializeField] private List<SerializedTransferTarget> giveTo = new List<SerializedTransferTarget>();

        private readonly Dictionary<ITransferTarget, IEnumerator> _transferRoutines =
            new Dictionary<ITransferTarget, IEnumerator>();

        public bool CanGiveTo(ITransferTarget to) => giveTo.Contains(to as SerializedTransferTarget);


        public void TransferTo(ITransferTarget target)
        {
            if (!_transferRoutines.TryAdd(target, TransferItemsRoutine(target)))
            {
                return;
            }
            
            StartCoroutine(_transferRoutines[target]);
        }
        
        
        private IEnumerator TransferItemsRoutine(ITransferTarget target)
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

                yield return TransferSingleItemRoutine(target, item);
                
                item = stackZone.GetLast(target.AcceptableResources);
            }

            _transferRoutines.Remove(target);
        }


        protected virtual bool NeedToBrakeTransferRoutine()
        {
            return false;
        }


        private IEnumerator TransferSingleItemRoutine(ITransferTarget target, ZoneItem item)
        {
            stackZone.Remove(item);
            target.Add(item);

            yield return new WaitForSeconds(0.2f);
        }
    }
}