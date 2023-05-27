using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using GenesisConstructa.SaveLoadSystem;
using UnityEngine;

namespace GenesisConstructa.TutorialSystem
{
    public class TutorialBuilder : MonoBehaviour, ISceneSaveable
    {
        [SerializeField] private TutorialStep[] order = (TutorialStep[]) Enum.GetValues(typeof(TutorialStep));

        [SerializeField, HideInInspector] private PermanentGuid guid;

        private TutorialStepTrackersSequence _trackersSequence;
        private object _trackersSequenceData;

        public bool Complete { get; private set; }

        public PermanentGuid Guid => guid;
        public int LoadingOrder => -101;
        
        public event Action Completed;
        

        public void Initialize()
        {
            TutorialStepTracker[] stepTrackers = FindObjectsOfType<TutorialStepTracker>(true).ToArray();

            SetItemsToTrackers(stepTrackers);
            
            InitializeTrackersSequence(stepTrackers);
        }


        private void SetItemsToTrackers(TutorialStepTracker[] stepTrackers)
        {
            TutorialItem[] tutorialItems = FindObjectsOfType<TutorialItem>(true);

            foreach (TutorialStepTracker tracker in stepTrackers)
            {
                tracker.SetItems(tutorialItems.Where(i => i.Step == tracker.Step).ToArray());
            }
        }


        private void InitializeTrackersSequence(TutorialStepTracker[] stepTrackers)
        {
            TutorialStepTracker[] sortedTrackers = stepTrackers.OrderBy(t => Array.IndexOf(order, t.Step)).ToArray();
            
            _trackersSequence = new TutorialStepTrackersSequence(sortedTrackers);
            _trackersSequence.Finished += OnSequenceFinished;

            if (_trackersSequenceData != null)
            {
                _trackersSequence.Load(_trackersSequenceData);
            }
            
            _trackersSequence.Activate();
        }


        private void OnSequenceFinished()
        {
            Complete = true;
            Completed?.Invoke();
            
            enabled = false;
        }


        private void OnDisable()
        {
            if (_trackersSequence == null)
            {
                return;
            }

            _trackersSequence.Finished -= OnSequenceFinished;
        }


        public void SubscribeToCurrentStepCompletion(Action action)
        {
            SubscribeToCurrentStepCompletionWhenReady(action).Forget();
        }


        private async UniTaskVoid SubscribeToCurrentStepCompletionWhenReady(Action action)
        {
            if (_trackersSequence == null)
            {
                await UniTask.WaitUntil(() => _trackersSequence != null).Timeout(TimeSpan.FromSeconds(2f));
            }
            
            _trackersSequence!.SubscribeToCurrentStepCompletion(action);
        }


        public object Save()
        {
            return new SaveData
            {
                StepTrackersSequenceData = _trackersSequence.Save(),
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;
            
            _trackersSequenceData = saveData.StepTrackersSequenceData;
        }


        [Serializable]
        private struct SaveData
        {
            public object StepTrackersSequenceData { get; set; }
        }
    }
}