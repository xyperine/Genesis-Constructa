using UnityEngine;

namespace ColonizationMobileGame.Player
{
    public class PlayerMovementSoundEffect : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private AudioSource audioSource;


        private void LateUpdate()
        {
            if (playerMovement.RelativeVelocity <= 0f)
            {
                audioSource.Stop();
                return;
            }

            audioSource.pitch = playerMovement.RelativeVelocity;
            
            if (audioSource.isPlaying)
            {
                return;
            }
            
            audioSource.Play();
        }
    }
}