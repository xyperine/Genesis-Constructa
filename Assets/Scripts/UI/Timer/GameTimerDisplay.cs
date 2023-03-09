using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ColonizationMobileGame.UI.Timer
{
    public class GameTimerDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text text;
        [SerializeField] private ColonizationMobileGame.Timer.GameTimer gameTimer;

        [Header("Critical Time")]
        [SerializeField] private Color criticalTimeColor;
        [SerializeField, Range(0f, 1f)] private float criticalTimeThreshold = 0.2f;
        [SerializeField, Min(0.1f)] private float criticalTimeAnimationDuration = 1f;

        [Header("Other")]
        [SerializeField] private GameTimerDisplayPrecision precision;

        private Tween _colorTween;
        private Color _normalColor;


        private void Awake()
        {
            _normalColor = text.color;
        }


        private void LateUpdate()
        {
            SetText();
        }


        private void SetText()
        {
            string format = GetFormat();
            string timeText = TimeSpan.FromSeconds(gameTimer.SecondsLeft).ToString(format);

            text.text = timeText;

            SetColor();
        }


        private string GetFormat()
        {
            return precision switch
            {
                GameTimerDisplayPrecision.Seconds => @"mm\:ss",
                GameTimerDisplayPrecision.Milliseconds => @"mm\:ss\:fff",
                _ => throw new ArgumentOutOfRangeException(),
            };
        }


        private void SetColor()
        {
            if (gameTimer.NormalizedTimeLeft > criticalTimeThreshold)
            {
                return;
            }

            if (_colorTween is {active: true})
            {
                return;
            }
            
            _colorTween = DOTween.Sequence()
                .Append(text.DOColor(criticalTimeColor, criticalTimeAnimationDuration))
                .Append(text.DOColor(_normalColor, criticalTimeAnimationDuration))
                .SetLoops(-1);
        }
    }
}