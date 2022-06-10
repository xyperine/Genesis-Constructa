using System;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using UnityEngine;

namespace ColonizationMobileGame.ObjectPooling
{
    [Serializable]
    public struct ItemPoolEntry
    {
        [SerializeField] private StackZoneItem itemPrefab;
        [SerializeField] private float weight;

        public StackZoneItem ItemPrefab => itemPrefab;
        public float Weight => weight;
    }
}