using System;
using UnityEngine;

namespace GenesisConstructa.ItemsExtraction.ConditionsLogic
{
    public class ExtractorConditionsUnit : MonoBehaviour
    {
        [SerializeField] private ExtractorConditionsSet workConditions;
        [SerializeField] private ExtractorConditionsSet productionConditions;

        public bool WorkConditionsMet => workConditions.Met;
        public bool ProductionConditionsMet => productionConditions.Met && workConditions.Met;

        public event Action ConditionsChanged;


        private void OnEnable()
        {
            workConditions.Changed += InvokeConditionsChanged;
            productionConditions.Changed += InvokeConditionsChanged;
        }


        private void OnDisable()
        {
            workConditions.Changed -= InvokeConditionsChanged;
            productionConditions.Changed -= InvokeConditionsChanged;
        }


        private void InvokeConditionsChanged()
        {
            ConditionsChanged?.Invoke();
        }
    }
}