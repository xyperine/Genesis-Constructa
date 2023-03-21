using ColonizationMobileGame.Structures;
using ColonizationMobileGame.UnlockingSystem;
using UnityEngine;

namespace ColonizationMobileGame.Audio
{
    public class UnlockSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private UnlockStation unlockStation;


        private void Awake()
        {
            unlockStation.Unlocked += Play;
        }


        private void Play(StructureIdentifier identifier)
        {
            audioSource.Play();
        }
    }
}