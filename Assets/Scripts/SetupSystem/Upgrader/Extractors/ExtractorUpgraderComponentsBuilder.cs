using System;
using GenesisConstructa.ItemsExtraction.Upgrading;
using UnityEngine;

namespace GenesisConstructa.SetupSystem.Upgrader.Extractors
{
    [Serializable]
    public class ExtractorUpgraderComponentsBuilder : UpgraderComponentsBuilder<ExtractorUpgraderSetupData>
    {
        [SerializeField] private ExtractorUpgrader upgrader;


        protected override void SetupItemsConsumer()
        {
            consumer.Setup(setupData.Chain.RequirementsChain);
        }


        protected override void SetupUpgrader()
        {
            upgrader.Construct(setupData);
            consumer.Consumed += upgrader.SetItemsAmountData;
            setupData.Chain.RequirementsChain.ChangingBlock += upgrader.SetItemsAmountData;
        }
    }
}