using GenesisConstructa.ItemsExtraction.Upgrading;
using UnityEngine;

namespace GenesisConstructa.Audio
{
    public class StructureActionSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private ExtractorUpgrader upgrader;


        private void Start()
        {
            upgrader.Chain.RequirementsChain.ChangingBlock += Play;
        }


        private void Play()
        {
            audioSource.Play();
        }
    }
}