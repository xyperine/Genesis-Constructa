using ColonizationMobileGame.GameOver;
using UnityEngine;

namespace ColonizationMobileGame.UI.GameOver
{
    public class GameOverScreen : MonoBehaviour, IGameOverTarget
    {
        [SerializeField] private GameOverText gameOverText;


        public void SubscribeToGameOver(GameOverManager gameOverManager)
        {
            gameOverManager.OverWithOutcome += Show;
        }


        public void Show(GameOutcome outcome)
        {
            gameObject.SetActive(true);
            
            gameOverText.SetText(outcome);
        }
    }
}