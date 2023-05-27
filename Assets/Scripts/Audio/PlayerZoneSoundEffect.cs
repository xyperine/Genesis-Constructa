using GenesisConstructa.Utility;
using UnityEngine;

namespace GenesisConstructa.Audio
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