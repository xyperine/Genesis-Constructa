using ColonizationMobileGame.GameFinalization;
using UnityEngine;

namespace ColonizationMobileGame.UI.GameOver
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameOverOutcomeText outcomeText;


        public void Show(GameOutcome outcome)
        {
            gameObject.SetActive(true);
            
            outcomeText.SetText(outcome);
        }
    }
}