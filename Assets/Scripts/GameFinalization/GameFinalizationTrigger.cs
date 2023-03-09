using System;
using UnityEngine;

namespace ColonizationMobileGame.GameFinalization
{
    public abstract class GameFinalizationTrigger : MonoBehaviour
    {
        [SerializeField] private GameOutcome outcome;

        public event Action<GameOutcome> Triggered;


        protected void Trigger()
        {
            Triggered?.Invoke(outcome);
        }
    }
}