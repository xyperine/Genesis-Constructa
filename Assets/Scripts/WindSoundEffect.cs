using System.Collections;
using ColonizationMobileGame.Utility.Helpers;
using UnityEngine;

namespace ColonizationMobileGame
{
    public class WindSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private float intervalInSeconds;
        [SerializeField, Range(0f, 1f)] private float chance;


        private void Start()
        {
            StartCoroutine(PlayRoutine());
        }


        private IEnumerator PlayRoutine()
        {
            while (true)
            {
                yield return YieldInstructionsHelpers.GetWaitForSeconds(intervalInSeconds);

                if (chance >= Random.value)
                {
                    audioSource.Play();
                }
            }
        }
    }
}