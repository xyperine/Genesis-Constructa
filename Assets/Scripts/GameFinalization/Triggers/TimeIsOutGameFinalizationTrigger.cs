using ColonizationMobileGame.Timer;
using UnityEngine;

namespace ColonizationMobileGame.GameFinalization.Triggers
{
    public class TimeIsOutGameFinalizationTrigger : GameFinalizationTrigger
    {
        [SerializeField] private GameTimer timer;


        private void Awake()
        {
            timer.Elapsed += Trigger;
        }
    }
}