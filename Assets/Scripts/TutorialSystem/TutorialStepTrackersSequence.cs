using System;
using System.Linq;

namespace ColonizationMobileGame.TutorialSystem
{
    public class TutorialStepTrackersSequence : IChain<TutorialStepTracker>
    {
        private readonly TutorialStepTracker[] _stepTrackers;
        private readonly TutorialStep _initialStep;
        
        public TutorialStepTracker Current { get; private set; }
        public TutorialStep CurrentStep => Current != null ? Current.Step : TutorialStep.Complete;

        public event Action ChangingTracker;
        public event Action ChangedTracker;
        

        public TutorialStepTrackersSequence(TutorialStepTracker[] stepTrackers, TutorialStep initialStep)
        {
            _stepTrackers = stepTrackers;
            _initialStep = initialStep;
            
            SetupSequence();
        }
        

        private void SetupSequence()
        {
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
            
            SubscribeToActiveTrackerCompletion();
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

            ChangingTracker?.Invoke();
            int nextIndex = Array.IndexOf(_stepTrackers, Current) + 1;
            if (nextIndex < 0 || nextIndex >= _stepTrackers.Length)
            {
                Current = null;
                return;
            }
            Current = _stepTrackers[nextIndex];

            SubscribeToActiveTrackerCompletion();
            
            ChangedTracker?.Invoke();
        }


        private void UnsubscribeFromActiveTrackerCompletion()
        {
            if (Current == null)
            {
                return;
            }

            Current.Completed -= GoToNextTracker;
        }
    }
}