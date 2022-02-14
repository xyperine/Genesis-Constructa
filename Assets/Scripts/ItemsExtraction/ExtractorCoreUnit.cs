using MoonPioneerClone.ItemsExtraction.Conditions;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public class ExtractorCoreUnit : MonoBehaviour
    {
        [SerializeField] private ExtractorProductionUnit _productionUnit;
        [SerializeField] private ExtractorConditionsUnit _conditionsUnit;
        [SerializeField] private ExtractorAnimator animator;


        private void OnEnable()
        {
            if (_conditionsUnit.WorkConditionsMet)
            {
                StartUp();
            }

            _conditionsUnit.WorkConditionsChanged += OnWorkConditionsChanged;
            _conditionsUnit.ProductionConditionsChanged += OnProductionConditionsChanged;
        }


        private void OnDisable()
        {
            _conditionsUnit.WorkConditionsChanged -= OnWorkConditionsChanged;
            _conditionsUnit.ProductionConditionsChanged -= OnProductionConditionsChanged;
        }


        private void StartUp()
        {
            animator.PlayStartUpAnimation();

            if (_conditionsUnit.ProductionConditionsMet)
            {
                _productionUnit.StartProduction();
            }
        }


        private void ShutDown()
        {
            animator.PlayShutDownAnimation();
            _productionUnit.StopProduction();
        }


        private void OnWorkConditionsChanged()
        {
            if (_conditionsUnit.WorkConditionsMet)
            {
                StartUp();
                return;
            }

            ShutDown();
        }
        
        
        private void OnProductionConditionsChanged()
        {
            if (_conditionsUnit.ProductionConditionsMet)
            {
                _productionUnit.StartProduction();
                return;
            }

            _productionUnit.StopProduction();
        }
    }
}