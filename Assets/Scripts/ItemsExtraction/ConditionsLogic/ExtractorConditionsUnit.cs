using System;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.ConditionsLogic
{
    public class ExtractorConditionsUnit : MonoBehaviour
    {
        [SerializeField] private ExtractorConditionsSet workConditions;
        [SerializeField] private ExtractorConditionsSet productionConditions;

        public bool WorkConditionsMet => workConditions.Met;
        public bool ProductionConditionsMet => productionConditions.Met && workConditions.Met;

        public event Action WorkConditionsChanged; 
        public event Action ProductionConditionsChanged; 


        private void OnEnable()
        {
            workConditions.Changed += InvokeWorkConditionsChanged;
            productionConditions.Changed += InvokeProductionConditionsChanged;
        }


        private void OnDisable()
        {
            workConditions.Changed -= InvokeWorkConditionsChanged;
            productionConditions.Changed -= InvokeProductionConditionsChanged;
        }


        private void InvokeWorkConditionsChanged()
        {
            WorkConditionsChanged?.Invoke();
        }


        private void InvokeProductionConditionsChanged()
        {
            ProductionConditionsChanged?.Invoke();
        }
    }
}