using System.Collections;
using ColonizationMobileGame.Timer;
using ColonizationMobileGame.Utility.Helpers;
using UnityEngine;

namespace ColonizationMobileGame.Audio
{
    public class GameTimerSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameTimer gameTimer;

        private bool _active;


        private void Update()
        {
            if (_active)
            {
                return;
            }
            
            if (gameTimer.GetPhase() == GameTimerPhase.Critical)
            {
                StartCoroutine(PlayRoutine());
            }
        }


        private IEnumerator PlayRoutine()
        {
            _active = true;
            
            while (true)
            {
                audioSource.Play();

                yield return YieldInstructionsHelpers.GetWaitForSeconds(1f);
            }
        }
    }
}