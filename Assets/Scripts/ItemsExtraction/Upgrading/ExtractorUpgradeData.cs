using System;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction.Upgrading
{
    [Serializable]
    public class ExtractorUpgradeData : StackZoneUpgradeData
    {
        [SerializeField] private float itemsPerSecond;

        public float ItemsPerSecond => itemsPerSecond;
    }
}