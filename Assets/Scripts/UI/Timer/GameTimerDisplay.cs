using System;
using DG.Tweening;
using GenesisConstructa.Timer;
using TMPro;
using UnityEngine;
using GameTimer = GenesisConstructa.Timer.GameTimer;

namespace GenesisConstructa.UI.Timer
{
    public class GameTimerDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text text;
        [SerializeField] private GameTimer gameTimer;

        [Header("Time Phases")]
        [SerializeField] private Color dangerousColor;
        [SerializeField, Min(0.1f)] private float dangerousTimeAnimationDuration = 1f;
        
        [SerializeField] private Color criticalTimeColor;
        [SerializeField, Min(0.1f)] private float criticalTimeAnimationDuration = 0.5f;
        
        [Header("Other")]
        [SerializeField] private TimePrecision precision;

        private readonly TimeFormatter _timeFormatter = new TimeFormatter();

        private GameTimerPhase _phase = GameTimerPhase.Normal;

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
            text.text = _timeFormatter.GetFormattedTime(gameTimer.SecondsLeft, precision);

            SetColor();
        }


        private void SetColor()
        {
            GameTimerPhase phase = gameTimer.GetPhase();
            
            if (phase == _phase)
            {
                return;
            }

            _phase = phase;
            
            (Color color, float animationDuration) = GetResult();

            if (_colorTween is {active: true})
            {
                _colorTween.Kill();
            }
            
            _colorTween = DOTween.Sequence()
                .Append(text.DOColor(color, animationDuration))
                .Append(text.DOColor(_normalColor, animationDuration))
                .SetLoops(-1);
        }
        
        
        private (Color, float) GetResult()
        {
            Color resultColor = default;
            float resultDuration = default;
            
            switch (_phase)
            {
                case GameTimerPhase.Normal:
                    break;
                case GameTimerPhase.Dangerous:
                    resultColor = dangerousColor;
                    resultDuration = dangerousTimeAnimationDuration;
                    break;
                case GameTimerPhase.Critical:
                    resultColor = criticalTimeColor;
                    resultDuration = criticalTimeAnimationDuration;
                    break;
                case GameTimerPhase.Over:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_phase), _phase, null);
            }

            return (resultColor, resultDuration);
        }
    }
}