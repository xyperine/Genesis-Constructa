using GenesisConstructa.ItemsExtraction.Upgrading;
using UnityEngine;

namespace GenesisConstructa.Audio
{
    public class StructureUpgradeSoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private ExtractorUpgrader upgrader;


        private void Start()
        {
            if (upgrader.Chain?.RequirementsChain == null)
            {
                return;
            }
            
            upgrader.Chain.RequirementsChain.ChangingBlock += Play;
        }


        private void Play()
        {
            audioSource.Play();
        }
    }
}