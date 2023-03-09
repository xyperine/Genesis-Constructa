using System;
using UnityEngine;

namespace ColonizationMobileGame.GameOver
{
    public class GameOverManager : MonoBehaviour
    {
        private GameOverTrigger[] _triggers;

        public event Action Over;
        public event Action<GameOutcome> OverWithOutcome; 


        public void SetTriggers(GameOverTrigger[] triggers)
        {
            foreach (GameOverTrigger trigger in triggers)
            {
                trigger.Triggered += End;
            }
        }


        private void End(GameOutcome outcome)
        {
            Over?.Invoke();
            OverWithOutcome?.Invoke(outcome);
        }
    }
}