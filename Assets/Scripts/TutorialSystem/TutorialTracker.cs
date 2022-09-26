using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem
{
    public class TutorialTracker : MonoBehaviour, ISceneSaveable
    {
        [SerializeField] private TutorialStep[] order = (TutorialStep[]) Enum.GetValues(typeof(TutorialStep));

        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private List<TutorialStepTracker> _stepTrackers;
        private List<TutorialItem> _tutorialItems;

        private readonly Dictionary<TutorialStep, Action> _actionsOnStepCompletion =
            Helpers.EnumToDictionary<TutorialStep, Action>(default(Action));

        private readonly Dictionary<TutorialStep, TutorialItem[]> _itemsMappedToSteps =
            Helpers.EnumToDictionary<TutorialStep, TutorialItem[]>(default(TutorialItem[]));

        private TutorialStep _currentStep;
        private int _currentStepIndex;

        private int _completedSteps;
        public bool Complete => _completedSteps >= order.Length;

        public PermanentGuid Guid => guid;
        public int LoadingOrder => -101;
        
        public event Action Completed;
        

        public void Initialize()
        {
            Wire();

            GoToNextStep();
        }


        private void Wire()
        {
            _stepTrackers = FindObjectsOfType<TutorialStepTracker>(true).ToList();
            _tutorialItems = FindObjectsOfType<TutorialItem>(true).ToList();

            foreach (TutorialStep step in _stepTrackers.Select(t => t.Step).Distinct())
            {
                _itemsMappedToSteps[step] = _tutorialItems.Where(i => i.Step == step).ToArray();
            }

            foreach (TutorialStepTracker stepTracker in _stepTrackers)
            {
                stepTracker.Completed += InvokeActionsOnStepCompletion;
            }
        }


        private void InvokeActionsOnStepCompletion(TutorialStep step)
        {
            Debug.Log(step);
            
            _actionsOnStepCompletion[step]?.Invoke();
            _actionsOnStepCompletion[step] = null;

            _completedSteps = Mathf.Min(_completedSteps + 1, order.Length);
            
            if (Complete)
            {
                Completed?.Invoke();
                enabled = false;
                return;
            }

            _currentStepIndex = _completedSteps;

            GoToNextStep();
        }


        private void GoToNextStep()
        {
            _currentStep = order[_currentStepIndex];

            TutorialItem[] items = _tutorialItems.Where(i => i.Step == _currentStep).ToArray();
            _stepTrackers.SingleOrDefault(t => t.Step == _currentStep)?.Activate(items);
        }


        private void OnDisable()
        {
            foreach (TutorialStepTracker stepTracker in _stepTrackers)
            {
                stepTracker.Completed -= InvokeActionsOnStepCompletion;
            }
        }


        public void SubscribeToCurrentStepCompletion(Action action)
        {
            if (_actionsOnStepCompletion[_currentStep] != null &&
                _actionsOnStepCompletion[_currentStep].GetInvocationList().Contains(action))
            {
                return;
            }
            
            _actionsOnStepCompletion[_currentStep] += action;
        }


        public object Save()
        {
            return new SaveData
            {
                CompletedSteps = _completedSteps,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            _completedSteps = saveData.CompletedSteps;

            if (!Complete)
            {
                Wire();
            }
        }


        [Serializable]
        private struct SaveData
        {
            public int CompletedSteps { get; set; }
        }
    }
}