using ColonizationMobileGame.ItemsExtraction.ConditionsLogic;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction
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

            conditionsUnit.ConditionsChanged += OnConditionsChanged;
            conditionsUnit.ConditionsChanged += OnProductionConditionsChanged;
        }


        private void OnDisable()
        {
            conditionsUnit.ConditionsChanged -= OnConditionsChanged;
            conditionsUnit.ConditionsChanged -= OnProductionConditionsChanged;
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


        private void OnConditionsChanged()
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