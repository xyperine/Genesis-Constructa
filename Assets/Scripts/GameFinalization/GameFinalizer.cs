using System;
using UnityEngine;

namespace ColonizationMobileGame.GameFinalization
{
    public class GameFinalizer : MonoBehaviour
    {
        private GameFinalizationTrigger[] _triggers;

        public event Action GameFinished;
        public event Action<GameOutcome> GameFinishedWithOutcome; 


        public void Initialize(GameFinalizationTrigger[] triggers)
        {
            _triggers = triggers;
            
            foreach (GameFinalizationTrigger trigger in _triggers)
            {
                trigger.Triggered += End;
            }
        }


        private void End(GameOutcome outcome)
        {
            GameFinished?.Invoke();
            GameFinishedWithOutcome?.Invoke(outcome);
            
            foreach (GameFinalizationTrigger trigger in _triggers)
            {
                trigger.Triggered -= End;
            }
        }
    }
}