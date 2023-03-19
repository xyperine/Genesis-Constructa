using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame.Player
{
    public class PlayerZoneSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Range pitchRange;


        public void Play()
        {
            audioSource.pitch = Random.Range(pitchRange.Min, pitchRange.Max);
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}