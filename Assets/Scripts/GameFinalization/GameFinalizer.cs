using System;
using UnityEngine;

namespace ColonizationMobileGame.GameFinalization
{
    public class GameFinalizer : MonoBehaviour
    {
        [SerializeField] private GameFinalizationEventSO finalizationEventSO;

        public event Action GameFinished;
        public event Action<GameOutcome> GameFinishedWithOutcome;


        private void OnEnable()
        {
            finalizationEventSO.Triggered += End;
        }


        private void End(GameOutcome outcome)
        {
            GameFinished?.Invoke();
            GameFinishedWithOutcome?.Invoke(outcome);
        }


        private void OnDisable()
        {
            finalizationEventSO.Triggered -= End;
        }
    }
}