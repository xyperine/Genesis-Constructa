using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.CollectableItemsInteractions.Split.Transfer
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
                yield return TransferSingleItemRoutine(target, item);
                
                item = stackZone.GetLast(target.AcceptableResources);
            }

            _transferRoutines.Remove(target);
        }


        private IEnumerator TransferSingleItemRoutine(ITransferTarget target, ZoneItem item)
        {
            if (NeedToBrakeTransferRoutine())
            {
                StopCoroutine(_transferRoutines[target]);
                _transferRoutines.Remove(target);
                yield break;
            }

            if (!target.CanTakeMore)
            {
                yield break;
            }
                
            yield return new WaitForSeconds(0.2f);

            stackZone.Remove(item);
            target.Add(item);
        }


        protected virtual bool NeedToBrakeTransferRoutine()
        {
            return false;
        }
    }
}