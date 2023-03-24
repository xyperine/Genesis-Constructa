﻿using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.GameFinalization;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.TutorialSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.Timer
{
    public class GameTimer : MonoBehaviour, ISceneSaveable, IGameFinalizationTarget
    {
        [SerializeField] private ShelterBuiltEventSO shelterBuiltEventSO;
        [SerializeField] private TutorialBuilder tutorialBuilder;
        
        [SerializeField, Range(10, 30)] private int minutes = 20;

        [SerializeField, HideInInspector] private PermanentGuid guid;

        private bool _active;

        public PermanentGuid Guid => guid;
        public int LoadingOrder => 10;

        public float SecondsLeft { get; private set; }

        public event Action Elapsed;


        private void Awake()
        {
            SecondsLeft += minutes * 60f;
        }


        private void OnEnable()
        {
            shelterBuiltEventSO.Built += Deactivate;
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


        public GameTimerPhase GetPhase()
        {
            IEnumerable<int> values = Enum.GetValues(typeof(GameTimerPhase)).Cast<int>();
            foreach (int value in values)
            {
                if (SecondsLeft <= value)
                {
                    return (GameTimerPhase) value;
                }
            }

            return default;
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
            shelterBuiltEventSO.Built -= Deactivate;
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


        [Button]
        private void Stop()
        {
            SecondsLeft = 1f;
        }
    }
}