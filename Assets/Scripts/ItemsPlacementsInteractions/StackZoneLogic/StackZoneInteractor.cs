using MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.Establisher;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic
{
    public abstract class StackZoneInteractor<TObject> : MonoBehaviour
    {
        [SerializeField] protected StackZoneActionsValidator validator;
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


        protected abstract void InteractWith(TObject obj);
    }
}