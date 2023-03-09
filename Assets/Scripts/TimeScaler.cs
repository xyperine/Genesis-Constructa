using ColonizationMobileGame.GameFinalization;
using UnityEngine;

namespace ColonizationMobileGame
{
    public class TimeScaler : MonoBehaviour, IGameFinalizationTarget
    {
        public void SubscribeToGameOver(GameFinalizer gameFinalizer)
        {
            gameFinalizer.GameFinished += StopTime;
        }


        private void StopTime()
        {
            Time.timeScale = 0f;
        }
    }
}