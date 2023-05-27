using GenesisConstructa.GameEvents;
using UnityEngine;

namespace GenesisConstructa.GameFinalization
{
    public class GameFinalizer : MonoBehaviour
    {
        [SerializeField] private GameFinalizationEventSO finalizationEventSO;
        [SerializeField] private GameFinalizationSequence finalizationSequence;


        private void OnEnable()
        {
            finalizationEventSO.Triggered += End;
        }


        private void End(GameOutcome outcome)
        {
            finalizationSequence.Begin(outcome);
        }


        private void OnDisable()
        {
            finalizationEventSO.Triggered -= End;
        }
    }
}