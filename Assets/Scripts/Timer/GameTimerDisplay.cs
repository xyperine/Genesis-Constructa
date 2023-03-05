using System;
using TMPro;
using UnityEngine;

namespace ColonizationMobileGame.Timer
{
    public class GameTimerDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private GameTimer gameTimer;


        private void LateUpdate()
        {
            text.text = TimeSpan.FromSeconds(gameTimer.SecondsLeft).ToString(@"mm\:ss\:fff");
        }
    }
}