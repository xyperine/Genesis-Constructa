using System;
using UnityEngine;

namespace MoonPioneerClone.UpgradesSystem.Upgrading
{
    [Serializable]
    public class ExtractorUpgradeData : UpgradeData
    {
        [SerializeField] private StackZoneUpgradeData stackZoneUpgradeData;
        [SerializeField] private ProductionUpgradeData productionUpgradeData;
    }
}