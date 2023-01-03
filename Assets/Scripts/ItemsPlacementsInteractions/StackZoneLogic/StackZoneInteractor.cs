using ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic
{
    public abstract class StackZoneInteractor<TObject> : MonoBehaviour, IInteractablesTrackerUser
        where TObject : MonoBehaviour
    {
        [SerializeField] protected InteractionsEstablisher establisher;
        [SerializeField, Range(0f, 10f)] private float scanRadius = 1f;

        private InteractablesTracker _interactablesTracker;
        
        protected abstract bool CanScan { get; }


        public void SetInteractablesTracker(InteractablesTracker interactablesTracker)
        {
            _interactablesTracker = interactablesTracker;
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
            
            int[] objectIDsInRadius = _interactablesTracker.GetObjectIDsInRadiusAround(transform.position, scanRadius);

            for (int i = 0; i < objectIDsInRadius.Length; i++)
            {
                TryToInteractWith(objectIDsInRadius[i]);
            }
        }


        private void TryToInteractWith(int objID)
        {
            if (!_interactablesTracker.IDsToObjectsMap.TryGetValue(objID, out MonoBehaviour value))
            {
                return;
            }

            if (value is not TObject)
            {
                return;
            }
            
            TObject obj = (TObject) _interactablesTracker.IDsToObjectsMap[objID];
            
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