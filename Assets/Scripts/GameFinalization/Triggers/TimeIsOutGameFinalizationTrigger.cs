using GenesisConstructa.Timer;
using UnityEngine;

namespace GenesisConstructa.GameFinalization.Triggers
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