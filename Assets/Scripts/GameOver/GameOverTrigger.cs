using System;
using UnityEngine;

namespace ColonizationMobileGame.GameOver
{
    public abstract class GameOverTrigger : MonoBehaviour
    {
        [SerializeField] private GameOutcome outcome;

        public event Action<GameOutcome> Triggered;


        protected void Trigger()
        {
            Triggered?.Invoke(outcome);
        }
    }
}