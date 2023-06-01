using GenesisConstructa.Player;
using GenesisConstructa.Utility;
using UnityEngine;

namespace GenesisConstructa.Audio
{
    public class PlayerMovementSoundEffect : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Range pitchRange;


        private void LateUpdate()
        {
            if (playerMovement.RelativeVelocity <= 0f)
            {
                audioSource.Stop();
                return;
            }

            audioSource.pitch = playerMovement.RelativeVelocity * pitchRange.Random();
            
            if (audioSource.isPlaying)
            {
                return;
            }
            
            audioSource.Play();
        }
    }
}