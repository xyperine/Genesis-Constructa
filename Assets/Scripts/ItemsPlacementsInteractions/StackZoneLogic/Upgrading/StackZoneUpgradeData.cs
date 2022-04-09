using System;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic.Upgrading
{
    [Serializable]
    public class StackZoneUpgradeData
    {
        [SerializeField] private int capacity;

        public int Capacity => capacity;
    }
}