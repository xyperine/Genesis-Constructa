using System.Collections;
using GenesisConstructa.Timer;
using GenesisConstructa.Utility.Helpers;
using UnityEngine;

namespace GenesisConstructa.Audio
{
    public class GameTimerSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameTimer gameTimer;

        private bool _active;

        private Coroutine _playRoutine;


        private void Update()
        {
            if (_active)
            {
                StopPlayingAfterTimeIsOut();
                
                return;
            }

            StartPlayingCriticalSound();
        }


        private void StopPlayingAfterTimeIsOut()
        {
            if (gameTimer.GetPhase() != GameTimerPhase.Over)
            {
                return;
            }

            if (_playRoutine != null)
            {
                StopCoroutine(_playRoutine);
            }
            
            _active = false;
        }


        private void StartPlayingCriticalSound()
        {
            if (gameTimer.GetPhase() == GameTimerPhase.Critical)
            {
                _playRoutine = StartCoroutine(PlayRoutine());
            }
        }


        private IEnumerator PlayRoutine()
        {
            _active = true;
            
            while (_active)
            {
                audioSource.Play();

                yield return YieldInstructionsHelpers.GetWaitForSeconds(1f);
            }
        }
    }
}