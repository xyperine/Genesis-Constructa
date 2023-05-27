using GenesisConstructa.InteractablesTracking;
using GenesisConstructa.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic
{
    public abstract class StackZoneInteractor<TObject, TBehaviour> : MonoBehaviour, IInteractablesTrackerUser
        where TObject : MonoBehaviour
        where TBehaviour : MonoBehaviour
    {
        [SerializeField] protected InteractionsEstablisher establisher;
        [SerializeField] protected TBehaviour behaviour;
        [SerializeField, Range(0f, 10f)] protected float scanRadius = 1f;

        private InteractablesTracker _interactablesTracker;
        
        protected abstract bool CanScan { get; }


        public void SetInteractablesTracker(InteractablesTracker interactablesTracker)
        {
            _interactablesTracker = interactablesTracker;
        }
        
        
        public void Setup(InteractionsEstablisher establisher, TBehaviour behaviour, float scanRadius)
        {
            this.establisher = establisher;
            this.behaviour = behaviour;
            this.scanRadius = scanRadius;
        }


        private void Update()
        { 
            Scan();
        }


        private void Scan()
        {
            if (!CanScan)
            {
                return;
            }

            if (!_interactablesTracker)
            {
                return;
            }
            
            MonoBehaviour[] objectsInRadius = _interactablesTracker.GetObjectsInRadiusAround(transform.position, scanRadius);

            for (int i = 0; i < objectsInRadius.Length; i++)
            {
                TryToInteractWith(objectsInRadius[i]);
            }
        }


        private void TryToInteractWith(MonoBehaviour @object)
        {
            if (@object is not TObject)
            {
                return;
            }
            
            TObject obj = (TObject) @object;
            
            InteractWith(obj);
        }


        protected abstract void InteractWith(TObject obj);


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, scanRadius);
        }
    }
}