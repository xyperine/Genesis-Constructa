using System;
using ColonizationMobileGame.GameOver;
using TMPro;
using UnityEngine;

namespace ColonizationMobileGame.UI.GameOver
{
    public class GameOverText : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI text;

        [Header("Win Case")]
        [SerializeField] private string winText = "Success!";
        [SerializeField] private Color winTextColor;
        
        [Header("Lose Case")]
        [SerializeField] private string loseText = "Fail!";
        [SerializeField] private Color loseTextColor;


        public void SetText(GameOutcome outcome)
        {
            (string resultText, Color resultColor) = GetResult(outcome);

            text.text = resultText;
            text.color = resultColor;
        }


        private (string, Color) GetResult(GameOutcome outcome)
        {
            string resultText;
            Color resultColor;
            
            switch (outcome)
            {
                case GameOutcome.Win:
                    resultText = winText;
                    resultColor = winTextColor;
                    break;
                case GameOutcome.Lose:
                    resultText = loseText;
                    resultColor = loseTextColor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(outcome), outcome, null);
            }

            return (resultText, resultColor);
        }
    }
}