﻿using GenesisConstructa.GameFading;
using GenesisConstructa.SaveLoadSystem;
using GenesisConstructa.Timer;
using GenesisConstructa.UI;
using GenesisConstructa.UI.GameOver;
using UnityEngine;

namespace GenesisConstructa.GameFinalization
{
    public class GameFinalizationSequence : MonoBehaviour
    {
        [SerializeField] private UIDisabler uiDisabler;
        [SerializeField] private GameOverScreen gameOverScreen;
        [SerializeField] private GameFader gameFader;
        [SerializeField] private SaveLoadManager saveLoadManager;
        [SerializeField] private GameTimer gameTimer;


        public void Begin(GameOutcome outcome)
        {
            if (outcome == GameOutcome.Win)
            {
                OnWin();
            }
            else
            {
                OnLose();
            }
        }


        private void OnWin()
        {
            gameFader.FadeOutImmediately(FadeFlags.Time);

            uiDisabler.DisableUI();
            gameOverScreen.Show(GameOutcome.Win, gameTimer.SecondsSpent);
            
            saveLoadManager.DisableSavingForThisSession();
        }


        private void OnLose()
        {
            gameFader.FadedOut += OnTimeStopped;
            gameFader.BeginFadeOut(3f, FadeFlags.All);
        }
        
        
        private void OnTimeStopped()
        {
            uiDisabler.DisableUI();
            gameOverScreen.Show(GameOutcome.Lose, gameTimer.SecondsSpent);
            
            saveLoadManager.DisableSavingForThisSession();
        }
    }
}