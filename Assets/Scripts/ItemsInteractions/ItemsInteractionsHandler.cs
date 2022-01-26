using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    [RequireComponent(typeof(StackZonesActionsValidator))]
    public abstract class ItemsInteractionsHandler : MonoBehaviour
    {
        protected StackZonesActionsValidator validator;


        private void Awake()
        {
            validator = GetComponent<StackZonesActionsValidator>();
        }
    }
}