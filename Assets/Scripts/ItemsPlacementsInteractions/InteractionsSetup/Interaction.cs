using System;
using GenesisConstructa.ItemsPlacementsInteractions.Target;
using GenesisConstructa.Utility.Observing;
using GenesisConstructa.Utility.Validating;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacementsInteractions.InteractionsSetup
{
    [Serializable]
    public class Interaction : IValidatable, IObservable
    {
        [SerializeField] private InteractionTarget target;
        [SerializeField] private InteractionType type;

        private InteractionTarget _prevTarget;
        private InteractionType _prevType;
        
        public InteractionTarget Target => target;
        public InteractionType Type => type;
        
        public event Action Changed;


        public Interaction(InteractionTarget target, InteractionType type)
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