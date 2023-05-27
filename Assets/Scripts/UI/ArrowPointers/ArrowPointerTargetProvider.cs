using UnityEngine;

namespace GenesisConstructa.UI.ArrowPointers
{
    public abstract class ArrowPointerTargetProvider<TGroupValidator> : MonoBehaviour
        where TGroupValidator : ArrowPointerTargetGroupValidator
    {
        [SerializeField] protected Transform transformToPointAt;


        protected void Register()
        {
            ArrowPointerTarget target = GetTarget();
            FindObjectOfType<TGroupValidator>().RegisterTarget(target);
        }


        protected abstract ArrowPointerTarget GetTarget();
    }
}