using System;
using System.Collections;
using GenesisConstructa.ItemsExtraction.ConditionsLogic;
using GenesisConstructa.Utility.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.Conveyor
{
    public class ConveyorPowerController : MonoBehaviour
    {
        [SerializeField] private ExtractorConditionsUnit conditionsUnit;
        [SerializeField] private bool workAfterGoingOffline;
        [SerializeField, ShowIf(nameof(workAfterGoingOffline)), Min(0f), Indent()] private float turnOffDelayInSeconds;

        public bool Powered { get; private set; }

        public event Action PoweredStatusChanged;


        private void OnEnable()
        {
            conditionsUnit.ConditionsChanged += OnConditionsChanged;
        }


        private void OnConditionsChanged()
        {
            if (conditionsUnit.WorkConditionsMet)
            {
                SetPoweredStatus(true);
                return;
            }
            
            if (workAfterGoingOffline)
            {
                StartCoroutine(DelayTurnOffRoutine());
            }
            else
            {
                SetPoweredStatus(false);
            }
        }


        private IEnumerator DelayTurnOffRoutine()
        {
            yield return YieldInstructionsHelpers.GetWaitForSeconds(turnOffDelayInSeconds);
            
            SetPoweredStatus(false);
        }


        private void SetPoweredStatus(bool status)
        {
            Powered = status;
            
            PoweredStatusChanged?.Invoke();
        }

        
        private void OnDisable()
        {
            conditionsUnit.ConditionsChanged -= OnConditionsChanged;
        }
    }
}