using MoonPioneerClone.ItemsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    [RequireComponent(typeof(StackZoneActionsValidator))]
    public abstract class ItemsInteractionsHandler : MonoBehaviour
    {
        protected StackZoneActionsValidator validator;


        private void Awake()
        {
            GetComponents();
        }


        private void GetComponents()
        {
            validator = GetComponent<StackZoneActionsValidator>();
        }
    }
}