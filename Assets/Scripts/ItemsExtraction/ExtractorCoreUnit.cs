using MoonPioneerClone.ItemsExtraction.ConditionsLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public class ExtractorCoreUnit : MonoBehaviour
    {
        [SerializeField] private ExtractorProductionUnit productionUnit;
        [SerializeField] private ExtractorConditionsUnit conditionsUnit;
        [SerializeField] private ExtractorAnimator animator;


        private void OnEnable()
        {
            if (conditionsUnit.WorkConditionsMet)
            {
                StartUp();
            }

            conditionsUnit.WorkConditionsChanged += OnWorkConditionsChanged;
            conditionsUnit.ProductionConditionsChanged += OnProductionConditionsChanged;
        }


        private void OnDisable()
        {
            conditionsUnit.WorkConditionsChanged -= OnWorkConditionsChanged;
            conditionsUnit.ProductionConditionsChanged -= OnProductionConditionsChanged;
        }


        private void StartUp()
        {
            animator.PlayStartUpAnimation();

            if (conditionsUnit.ProductionConditionsMet)
            {
                productionUnit.StartProduction();
            }
        }


        private void ShutDown()
        {
            animator.PlayShutDownAnimation();
            productionUnit.StopProduction();
        }


        private void OnWorkConditionsChanged()
        {
            if (conditionsUnit.WorkConditionsMet)
            {
                StartUp();
                return;
            }
            
            ShutDown();
        }
        
        
        private void OnProductionConditionsChanged()
        {
            if (conditionsUnit.ProductionConditionsMet)
            {
                productionUnit.StartProduction();
                return;
            }

            productionUnit.StopProduction();
        }
    }
}