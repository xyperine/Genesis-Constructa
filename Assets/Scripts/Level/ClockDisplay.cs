using System;
using TMPro;
using UnityEngine;

namespace ColonizationMobileGame.Level
{
    public class ClockDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private Clock clock;


        private void LateUpdate()
        {
            text.text = TimeSpan.FromSeconds(clock.ElapsedTimeInSeconds).ToString(@"hh\:mm\:ss");
        }
    }
}