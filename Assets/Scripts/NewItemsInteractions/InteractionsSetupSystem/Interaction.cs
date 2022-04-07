using System;
using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using MoonPioneerClone.Utility;
using MoonPioneerClone.Utility.Observing;
using MoonPioneerClone.Utility.Validating;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.InteractionsSetupSystem
{
    [Serializable]
    public class Interaction : IValidatable, IObservable
    {
        [SerializeField] private TransferTarget target;
        [SerializeField] private StackZoneInteractionType type;

        private TransferTarget _prevTarget;
        private StackZoneInteractionType _prevType;
        
        public TransferTarget Target => target;
        public StackZoneInteractionType Type => type;
        
        public event Action Changed;


        public Interaction(TransferTarget target, StackZoneInteractionType type)
        {
            this.target = target;
            this.type = type;

            _prevTarget = target;
            _prevType = type;
        }


        public void OnValidate()
        {
            if (_prevTarget == target && _prevType == type)
            {
                return;
            }

            Changed?.Invoke();

            _prevTarget = target;
            _prevType = type;
        }
    }
}