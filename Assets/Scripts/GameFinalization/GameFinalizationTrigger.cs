using GenesisConstructa.GameEvents;
using UnityEngine;

namespace GenesisConstructa.GameFinalization
{
    public abstract class GameFinalizationTrigger : MonoBehaviour
    {
        [SerializeField] private GameFinalizationEventSO finalizationEventSO;
        
        [SerializeField] private GameOutcome outcome;


        protected void Trigger()
        {
            finalizationEventSO.Raise(outcome);
        }
    }
}