using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.ScoreSystem;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using UnityEngine;

namespace ColonizationMobileGame.UnlockingSystem
{
    public class UnlockStation : MonoBehaviour, IItemsAmountDataProvider
    {
        [SerializeField] private ItemsAmountPanelData itemsAmountPanelData;
        [SerializeField] private ItemsConsumer consumer;
        [SerializeField] private UnlocksChainSO chainSO;

        [SerializeField] private ScoreModifier scoreModifier; 


        private void Start()
        {
            consumer.Setup(chainSO.RequirementsChain);
            consumer.Consumed += SetItemsAmountData;
            
            chainSO.RequirementsChain.ChangingBlock += SetItemsAmountData;
            chainSO.RequirementsChain.ChangingBlock += AddScore;

            SetItemsAmountData();
        }
        

        public void SetItemsAmountData()
        {
            Unlock current = chainSO.Current;

            itemsAmountPanelData.SetData(current?.Price.ToItemsAmount());
            itemsAmountPanelData.SetIdentifier(current?.Identifier);

            itemsAmountPanelData.InvokeChanged();
        }


        private void AddScore()
        {
            scoreModifier.Add(chainSO.Current.Identifier.StructureType);
        }
    }
}