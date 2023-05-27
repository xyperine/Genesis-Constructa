using System;
using GenesisConstructa.ItemsPlacementsInteractions;
using UnityEngine;

namespace GenesisConstructa.ObjectPooling
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