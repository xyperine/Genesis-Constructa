using System;
using UnityEngine;

namespace MoonPioneerClone.UpgradesSystem.Upgrading
{
    [Serializable]
    public class ProductionUpgradeData : UpgradeData
    {
        [SerializeField] private float itemsPerSecond;

        public float ItemsPerSecond => itemsPerSecond;
    }
}