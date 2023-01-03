using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.Target
{
    public sealed class InteractionTargetReference : MonoBehaviour, IInteractablesTrackerUser
    {
        [SerializeField] private InteractionTarget target;

        private InteractablesTracker _interactablesTracker;
        
        public InteractionTarget Target => target;


        public void Setup(InteractionTarget target)
        {
            this.target = target;
        }


        public void SetInteractablesTracker(InteractablesTracker interactablesTracker)
        {
            _interactablesTracker = interactablesTracker;
            
            _interactablesTracker.RegisterObject(this);
        }


        private void Start()
        {
            if (!_interactablesTracker)
            {
                return;
            }
            
            _interactablesTracker.RegisterObject(this);
        }
    }
}
