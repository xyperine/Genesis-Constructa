using System;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace GenesisConstructa.ItemsExtraction.Upgrading
{
    [Serializable]
    public class ExtractorUpgradeData : StackZoneUpgradeData
    {
        [SerializeField] private float itemsPerSecond;

        public float ItemsPerSecond => itemsPerSecond;
    }
}