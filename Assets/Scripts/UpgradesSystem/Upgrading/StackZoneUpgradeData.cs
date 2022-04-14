using System;
using UnityEngine;

namespace MoonPioneerClone.UpgradesSystem.Upgrading
{
    [Serializable]
    public class StackZoneUpgradeData : UpgradeData
    {
        [SerializeField] private int capacity;

        public int Capacity => capacity;
    }
}