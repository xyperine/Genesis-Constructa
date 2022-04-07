using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.StackZoneLogic
{
    public abstract class StackZoneInteractor<TObject, THandler> : MonoBehaviour
        where THandler : ItemsInteractionsHandler
    {
        [SerializeField] protected THandler handler;


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