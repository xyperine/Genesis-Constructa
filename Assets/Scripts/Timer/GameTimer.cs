using System;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.Timer
{
    public class GameTimer : MonoBehaviour, ISceneSaveable
    {
        [SerializeField, Range(10, 30)] private int minutes = 20;
        
        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private bool _active;

        public PermanentGuid Guid => guid;
        public int LoadingOrder => 10;

        public float SecondsLeft { get; private set; }

        public float NormalizedTimeLeft => SecondsLeft / 60f / minutes;


        private void Start()
        {
            SecondsLeft += minutes * 60;
            
            Activate();
        }


        public void Activate()
        {
            _active = true;
        }


        private void Update()
        {
            if (!_active)
            {
                return;
            }

            SecondsLeft -= Time.deltaTime;

            if (!(SecondsLeft <= 0f))
            {
                return;
            }

            Deactivate();

            Time.timeScale = 0f;
            Debug.LogWarning("The time is out!");
        }


        public void Deactivate()
        {
            _active = false;
        }


        private void OnDisable()
        {
            Debug.LogWarning($"Time left: {TimeSpan.FromSeconds(SecondsLeft):mm\\:ss\\:fff}");
        }


        public object Save()
        {
            return new SaveData
            {
                ElapsedTimeInSeconds = SecondsLeft,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            SecondsLeft = saveData.ElapsedTimeInSeconds;
        }
        
        
        private struct SaveData
        {
            public float ElapsedTimeInSeconds { get; set; }
        }
    }
}