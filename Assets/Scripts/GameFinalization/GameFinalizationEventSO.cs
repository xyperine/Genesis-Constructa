using System;
using UnityEngine;

namespace ColonizationMobileGame.GameFinalization
{
    [CreateAssetMenu(fileName = "Game_Finalization_Event", menuName = "Game Events/Game Finalization Event", order = 0)]
    public class GameFinalizationEventSO : ScriptableObject
    {
        public event Action<GameOutcome> Triggered;
        
        
        public void Raise(GameOutcome outcome)
        {
            Triggered?.Invoke(outcome);
        }
    }
}