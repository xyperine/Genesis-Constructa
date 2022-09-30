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

        private TutorialStepTrackersSequence _trackersSequence;
        
        private List<TutorialStepTracker> _stepTrackers;
        private List<TutorialItem> _tutorialItems;

        private readonly Dictionary<TutorialStep, Action> _actionsOnStepCompletion =
            Helpers.EnumToDictionary<TutorialStep, Action>(default(Action));

        private readonly Dictionary<TutorialStep, TutorialItem[]> _itemsMappedToSteps =
            Helpers.EnumToDictionary<TutorialStep, TutorialItem[]>(default(TutorialItem[]));

        private TutorialStep _currentStep;
        public bool Complete => _currentStep == TutorialStep.Complete;

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

            _trackersSequence = new TutorialStepTrackersSequence(
                _stepTrackers.OrderBy(t => Array.IndexOf(order, t.Step)).ToArray(), _currentStep);
            _trackersSequence.ChangingTracker += InvokeActionsOnStepCompletion;
            _trackersSequence.ChangedTracker += GoToNextStep;
        }


        private void InvokeActionsOnStepCompletion()
        {
            Debug.Log(_currentStep);
            
            _actionsOnStepCompletion[_currentStep]?.Invoke();
            _actionsOnStepCompletion[_currentStep] = null;

            if (Complete)
            {
                Completed?.Invoke();
                enabled = false;
            }
        }


        private void GoToNextStep()
        {
            _currentStep = _trackersSequence.CurrentStep;

            TutorialItem[] items = _itemsMappedToSteps[_currentStep];
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
                LastStepCompleted = _trackersSequence.CurrentStep,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;
            
            _currentStep = saveData.LastStepCompleted;
        }


        [Serializable]
        private struct SaveData
        {
            public TutorialStep LastStepCompleted { get; set; }
        }
    }
}