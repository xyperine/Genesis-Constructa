using System;
using MoonPioneerClone.ItemsPlacementsInteractions;
using UnityEngine;

namespace MoonPioneerClone.ObjectPooling
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