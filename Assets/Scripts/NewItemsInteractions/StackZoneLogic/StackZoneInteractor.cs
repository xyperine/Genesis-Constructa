using MoonPioneerClone.NewItemsInteractions.InteractionsSetupSystem;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.StackZoneLogic
{
    public abstract class StackZoneInteractor<TObject> : MonoBehaviour
    {
        [SerializeField] protected StackZoneActionsValidator validator;
        [SerializeField] protected InteractionsResolver resolver;


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