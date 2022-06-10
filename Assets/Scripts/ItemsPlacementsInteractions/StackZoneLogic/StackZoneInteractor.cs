using ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic
{
    public abstract class StackZoneInteractor<TObject> : MonoBehaviour
    {
        [SerializeField] protected InteractionsEstablisher establisher;


        private void OnTriggerEnter(Collider other)
        {
            TObject obj;
            if (!other.TryGetComponent(out obj))
            {
                return;
            }
            
            InteractWith(obj);
        }
        
        
        private void OnTriggerStay(Collider other)
        {
            TObject obj;
            if (!other.TryGetComponent(out obj))
            {
                return;
            }

            InteractWith(obj);
        }


        protected abstract void InteractWith(TObject obj);
    }
}