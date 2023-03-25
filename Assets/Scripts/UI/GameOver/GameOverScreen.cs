using ColonizationMobileGame.GameFinalization;
using UnityEngine;

namespace ColonizationMobileGame.UI.GameOver
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameOverText gameOverText;


        public void Show(GameOutcome outcome)
        {
            gameObject.SetActive(true);
            
            gameOverText.SetText(outcome);
        }
    }
}