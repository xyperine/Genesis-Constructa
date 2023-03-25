using ColonizationMobileGame.GameEvents;
using UnityEngine;

namespace ColonizationMobileGame.GameFinalization
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