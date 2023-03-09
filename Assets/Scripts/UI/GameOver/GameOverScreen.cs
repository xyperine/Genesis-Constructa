using ColonizationMobileGame.GameFinalization;
using UnityEngine;

namespace ColonizationMobileGame.UI.GameOver
{
    public class GameOverScreen : MonoBehaviour, IGameFinalizationTarget
    {
        [SerializeField] private GameOverText gameOverText;


        public void SubscribeToGameOver(GameFinalizer gameFinalizer)
        {
            gameFinalizer.GameFinishedWithOutcome += Show;
        }


        public void Show(GameOutcome outcome)
        {
            gameObject.SetActive(true);
            
            gameOverText.SetText(outcome);
        }
    }
}