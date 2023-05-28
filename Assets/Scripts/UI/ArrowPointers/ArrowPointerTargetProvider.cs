using UnityEngine;

namespace GenesisConstructa.UI.ArrowPointers
{
    public abstract class ArrowPointerTargetProvider<TGroupValidator> : MonoBehaviour
        where TGroupValidator : ArrowPointerTargetGroupValidator
    {
        [SerializeField] protected Transform transformToPointAt;
        
        private ArrowPointerTarget _target;
        private TGroupValidator _groupValidator;
        

        protected void Register()
        {
            _target = GetTarget();
            _groupValidator = FindObjectOfType<TGroupValidator>();
            
            _groupValidator.RegisterTarget(_target);
        }


        protected void Unregister()
        {
            _groupValidator?.UnregisterTarget(_target);
        }


        protected abstract ArrowPointerTarget GetTarget();
    }
}