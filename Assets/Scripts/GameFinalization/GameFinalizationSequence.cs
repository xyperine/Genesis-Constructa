using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.UI;
using ColonizationMobileGame.UI.GameOver;
using UnityEngine;

namespace ColonizationMobileGame.GameFinalization
{
    public class GameFinalizationSequence : MonoBehaviour
    {
        [SerializeField] private UIDisabler uiDisabler;
        [SerializeField] private GameOverScreen gameOverScreen;
        [SerializeField] private TimeScaler timeScaler;
        [SerializeField] private SaveLoadManager saveLoadManager;


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
            timeScaler.StopTime();

            uiDisabler.DisableUI();
            gameOverScreen.Show(GameOutcome.Win);
            
            saveLoadManager.DisableSavingForThisSession();
        }


        private void OnLose()
        {
            uiDisabler.DisableUI();

            timeScaler.TimeStopped += OnTimeStopped;
            timeScaler.BeginSlowDown();
        }
        
        
        private void OnTimeStopped()
        {
            gameOverScreen.Show(GameOutcome.Lose);
            
            saveLoadManager.DisableSavingForThisSession();
        }
    }
}