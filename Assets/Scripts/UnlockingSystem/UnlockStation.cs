using ColonizationMobileGame.ItemsPlacementsInteractions;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    public class UnlockStation : MonoBehaviour
    {
        [SerializeField] private ItemsConsumer consumer;
        [SerializeField] private UnlocksChainSO chainSO;

        
        private void Start()
        {
            consumer.Setup(chainSO.RequirementsChain);
        }
    }
}