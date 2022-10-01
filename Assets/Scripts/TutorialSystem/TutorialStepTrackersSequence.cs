using System;
using System.Linq;
using ColonizationMobileGame.SaveLoadSystem;

namespace ColonizationMobileGame.TutorialSystem
{
    public class TutorialStepTrackersSequence : IChain<TutorialStepTracker>, ISaveable
    {
        private readonly TutorialStepTracker[] _stepTrackers;
        private TutorialStep _initialStep;
        
        public TutorialStepTracker Current { get; private set; }
        private TutorialStep CurrentStep => Current != null ? Current.Step : TutorialStep.Complete;
        
        public event Action Finished;


        public TutorialStepTrackersSequence(TutorialStepTracker[] stepTrackers)
        {
            _stepTrackers = stepTrackers;
        }


        public void Activate()
        {
            if (_initialStep == TutorialStep.Complete)
            {
                Finished?.Invoke();
                return;
            }
            
            if (!_stepTrackers.Any())
            {
                return;
            }
            
            SetupInitialTracker();
        }


        private void SetupInitialTracker()
        {
            int index = Array.FindIndex(_stepTrackers, t => t.Step == _initialStep);
            Current = index == -1 ? null : _stepTrackers[index];
            
            SetupCurrentTracker();
        }


        private void SetupCurrentTracker()
        {
            if (!Current)
            {
                return;
            }
            
            SubscribeToActiveTrackerCompletion();

            Current.Activate();
        }


        private void SubscribeToActiveTrackerCompletion()
        {
            if (Current == null)
            {
                return;
            }

            Current.Completed += GoToNextTracker;
        }


        private void GoToNextTracker()
        {
            UnsubscribeFromActiveTrackerCompletion();

            SetNewCurrentTracker();

            SetupCurrentTracker();
        }


        private void UnsubscribeFromActiveTrackerCompletion()
        {
            if (Current == null)
            {
                return;
            }

            Current.Completed -= GoToNextTracker;
        }


        private void SetNewCurrentTracker()
        {
            int nextIndex = Array.IndexOf(_stepTrackers, Current) + 1;
            if (nextIndex < 0 || nextIndex >= _stepTrackers.Length)
            {
                Current = null;
                Finished?.Invoke();
                return;
            }
            
            Current = _stepTrackers[nextIndex];
        }


        public void SubscribeToCurrentStepCompletion(Action action)
        {
            if (!Current)
            {
                return;
            }

            Current.Completed += action;
        }


        public object Save()
        {
            return new SaveData
            {
                CurrentStep = CurrentStep,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;
            
            _initialStep = saveData.CurrentStep;
        }
        
        
        [Serializable]
        private struct SaveData
        {
            public TutorialStep CurrentStep { get; set; }
        }
    }
}