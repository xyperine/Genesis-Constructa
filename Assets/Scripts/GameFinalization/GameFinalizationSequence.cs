using ColonizationMobileGame.GameFading;
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
        [SerializeField] private GameFader gameFader;
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
            gameFader.FadeOutImmediately(FadeFlags.Time);

            uiDisabler.DisableUI();
            gameOverScreen.Show(GameOutcome.Win);
            
            saveLoadManager.DisableSavingForThisSession();
        }


        private void OnLose()
        {
            uiDisabler.DisableUI();
            
            gameFader.FadedOut += OnTimeStopped;
            gameFader.BeginFadeOut(3f, FadeFlags.All);
        }
        
        
        private void OnTimeStopped()
        {
            gameOverScreen.Show(GameOutcome.Lose);
            
            saveLoadManager.DisableSavingForThisSession();
        }
    }
}