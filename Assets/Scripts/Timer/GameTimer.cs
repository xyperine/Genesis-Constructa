using System;
using ColonizationMobileGame.GameFinalization;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.TutorialSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.Timer
{
    public class GameTimer : MonoBehaviour, ISceneSaveable, IGameFinalizationTarget
    {
        [SerializeField] private TutorialBuilder tutorialBuilder;
        [SerializeField, Range(10, 30)] private int minutes = 20;
        
        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private bool _active;

        public PermanentGuid Guid => guid;
        public int LoadingOrder => 10;

        public float SecondsLeft { get; private set; }

        public float NormalizedTimeLeft => SecondsLeft / 60f / minutes;

        public event Action Elapsed;


        [Button]
        public void Stop()
        {
            SecondsLeft = 1f;
        }
        
        
        private void Awake()
        {
            SecondsLeft += minutes * 60f;
        }


        private void Start()
        {
            if (tutorialBuilder.Complete)
            {
                Activate();
            }
            else
            {
                tutorialBuilder.Completed += Activate;
            }
        }


        public void Activate()
        {
            _active = true;

            tutorialBuilder.Completed -= Activate;
        }


        private void Update()
        {
            if (!_active)
            {
                return;
            }

            SecondsLeft -= Time.deltaTime;

            if (SecondsLeft <= 0f)
            {
                OnTimeIsUp();
            }
        }


        private void OnTimeIsUp()
        {
            SecondsLeft = 0f;
            
            Elapsed?.Invoke();

            Deactivate();
            
            Debug.LogWarning("The time is out!");
        }


        public void Deactivate()
        {
            _active = false;
        }


        private void OnDisable()
        {
            Debug.LogWarning($"Time left: {TimeSpan.FromSeconds(SecondsLeft):mm\\:ss\\:fff}");

            tutorialBuilder.Completed -= Activate;
        }


        public object Save()
        {
            return new SaveData
            {
                SecondsLeft = SecondsLeft,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            SecondsLeft = saveData.SecondsLeft;
        }
        
        
        private struct SaveData
        {
            public float SecondsLeft { get; set; }
        }

        public void SubscribeToGameOver(GameFinalizer gameFinalizer)
        {
            gameFinalizer.GameFinished += Deactivate;
        }
    }
}