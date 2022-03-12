using System;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.StackZoneLogic.Upgrading
{
    [Serializable]
    public class StackZoneUpgradeData
    {
        [SerializeField] private int capacity;

        public int Capacity => capacity;
    }
}