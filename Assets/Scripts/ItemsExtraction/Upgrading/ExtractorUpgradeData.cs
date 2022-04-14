using System;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction.Upgrading
{
    [Serializable]
    public class ExtractorUpgradeData// : StackZoneUpgradeData
    {
        [SerializeField] private float itemsPerSecond;

        public float ItemsPerSecond => itemsPerSecond;
    }
}