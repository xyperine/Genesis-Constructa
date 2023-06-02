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

        private bool _delayActive;
        private Coroutine _delayCoroutine;
        
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
                TurnOn();
                return;
            }

            TurnOff();
        }


        private void TurnOn()
        {
            if (_delayActive)
            {
                StopCoroutine(_delayCoroutine);
                _delayCoroutine = null;

                _delayActive = false;
            }
            
            SetPoweredStatus(true);
        }


        private void TurnOff()
        {
            if (workAfterGoingOffline)
            {
                if (!_delayActive)
                {
                    _delayCoroutine = StartCoroutine(DelayTurnOffRoutine());
                }
            }
            else
            {
                SetPoweredStatus(false);
            }
        }


        private IEnumerator DelayTurnOffRoutine()
        {
            _delayActive = true;
            
            yield return YieldInstructionsHelpers.GetWaitForSeconds(turnOffDelayInSeconds);
            
            _delayActive = false;
            
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