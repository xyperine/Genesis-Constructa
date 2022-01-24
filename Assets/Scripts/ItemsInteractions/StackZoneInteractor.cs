using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    public abstract class StackZoneInteractor<T> : MonoBehaviour
    {
        [SerializeField] protected ResourcesInteractionsHandler handler;


        private void OnTriggerEnter(Collider other)
        {
            T @object;
            if (!other.TryGetComponent(out @object))
            {
                return;
            }
            
            InteractWith(@object);
        }


        protected abstract void InteractWith(T @object);
    }
}