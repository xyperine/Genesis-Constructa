using ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic
{
    public abstract class StackZoneInteractor<TObject> : MonoBehaviour
    {
        [SerializeField] protected InteractionsEstablisher establisher;
        [SerializeField, Range(0f, 10f)] private float scanRadius = 1f;

        protected abstract bool Valid { get; }

        
        private void Update()
        {
            if (!CanScan())
            {
                return;
            }
            
            int[] objectIDsInRadius = Interactables.GetObjectIDsInRadiusAround(transform.position, scanRadius);

            for (int i = 0; i < objectIDsInRadius.Length; i++)
            {
                InteractWith(objectIDsInRadius[i]);
            }
        }


        private bool CanScan()
        {
            return Valid;
        }


        protected abstract void InteractWith(int objID);


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, scanRadius);
        }
    }
}