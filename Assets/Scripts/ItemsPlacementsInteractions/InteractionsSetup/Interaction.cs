using System;
using MoonPioneerClone.ItemsPlacementsInteractions.Transfer.Target;
using MoonPioneerClone.Utility.Observing;
using MoonPioneerClone.Utility.Validating;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup
{
    [Serializable]
    public class Interaction : IValidatable, IObservable
    {
        [SerializeField] private TransferTarget target;
        [SerializeField] private InteractionType type;

        private TransferTarget _prevTarget;
        private InteractionType _prevType;
        
        public TransferTarget Target => target;
        public InteractionType Type => type;
        
        public event Action Changed;


        public Interaction(TransferTarget target, InteractionType type)
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