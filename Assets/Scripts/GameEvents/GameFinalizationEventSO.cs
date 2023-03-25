using System;
using ColonizationMobileGame.GameFinalization;
using UnityEngine;

namespace ColonizationMobileGame.GameEvents
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