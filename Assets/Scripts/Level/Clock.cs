using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.Level
{
    public class Clock : MonoBehaviour, ISceneSaveable
    {
        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private bool _active;

        public PermanentGuid Guid => guid;
        public int LoadingOrder => 10;

        public float ElapsedTimeInSeconds { get; private set; }


        private void Start()
        {
            Activate();
        }


        public void Activate()
        {
            _active = true;
        }


        private void Update()
        {
            if (_active)
            {
                ElapsedTimeInSeconds += Time.deltaTime;
            }
        }


        public object Save()
        {
            return new SaveData
            {
                ElapsedTimeInSeconds = ElapsedTimeInSeconds,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            ElapsedTimeInSeconds = saveData.ElapsedTimeInSeconds;
        }
        
        
        private struct SaveData
        {
            public float ElapsedTimeInSeconds { get; set; }
        }
    }
}