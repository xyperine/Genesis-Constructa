using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Placements.Grid
{
    [Serializable]
    public class GridPlacementSizeSettings
    {
        [Header("Size (in items)")]
        [SerializeField] private Vector3Int defaultSize;

        [SerializeField] private bool useUpgrades;
        [SerializeField, ShowIf(nameof(useUpgrades))] private Vector3Int[] sizes;

        private IEnumerator sizesEnumerator;

        public Vector3Int DefaultSize => defaultSize;


        public Vector3 GetNextSize()
        {
            sizesEnumerator ??= sizes.GetEnumerator();

            if (!sizesEnumerator.MoveNext())
            {
                return Vector3.zero;
            }

            return (Vector3Int) sizesEnumerator.Current;
        }
    }
}