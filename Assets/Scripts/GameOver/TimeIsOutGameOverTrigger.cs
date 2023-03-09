using ColonizationMobileGame.Timer;
using UnityEngine;

namespace ColonizationMobileGame.GameOver
{
    public class TimeIsOutGameOverTrigger : GameOverTrigger
    {
        [SerializeField] private GameTimer timer;


        private void Start()
        {
            timer.Elapsed += Trigger;
        }
    }
}