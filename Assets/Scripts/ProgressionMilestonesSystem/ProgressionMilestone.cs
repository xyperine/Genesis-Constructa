using System;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.ProgressionMilestonesSystem
{
    public abstract class ProgressionMilestone : MonoBehaviour, IProgressionMilestone, ISaveableWithGuid
    {
        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        protected abstract ProgressionMilestoneType Type { get; }

        public bool IsAchieved { get; private set; }
        public PermanentGuid Guid => guid;
        
        public event Action<ProgressionMilestoneType> Achieved;


        private void OnEnable()
        {
            Subscribe();
        }


        protected abstract void Subscribe();


        private void OnDisable()
        {
            Unsubscribe();
        }


        protected abstract void Unsubscribe();


        protected void InvokeAchieved()
        {
            Achieved?.Invoke(Type);
            IsAchieved = true;

            gameObject.SetActive(false);
        }


        public object Save()
        {
            return new SaveData
            {
                IsAchieved = IsAchieved,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            IsAchieved = saveData.IsAchieved;
            if (IsAchieved)
            {
                //InvokeAchieved();
            }
        }
        
        
        [Serializable]
        private struct SaveData
        {
            public bool IsAchieved { get; set; }
        }
    }
}