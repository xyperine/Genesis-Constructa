using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GenesisConstructa.UI.GameOver
{
    public class GameOverTimeSpentText : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI text;

        [Header("Misc")] 
        [SerializeField] private Color color;
        [SerializeField, Min(0f)] private float fadeInDurationInSeconds = 2f;

        private readonly TimeFormatter _timeFormatter = new TimeFormatter();


        public void SetText(float timeSpent)
        {
            string hexColor = ColorUtility.ToHtmlStringRGBA(color);
            string formattedTimeSpent = _timeFormatter.GetFormattedTime(timeSpent, TimePrecision.Milliseconds);
            text.text = $"You've managed to do it in <color=#{hexColor}>{formattedTimeSpent}</color>!";
            
            text.alpha = 0f;
            text.DOFade(1f, fadeInDurationInSeconds).SetUpdate(true);
        }
    }
}