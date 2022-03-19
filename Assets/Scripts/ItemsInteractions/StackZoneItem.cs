using MoonPioneerClone.ItemsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacement;
using MoonPioneerClone.ItemsPlacement.Core;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    [RequireComponent(typeof(PlacementItem))]
    public sealed class StackZoneItem : MonoBehaviour
    {
        [SerializeField] private ItemType type;

        private PlacementItem _placementBehavior;

        public bool Moving => _placementBehavior.Moving;
        public ItemType Type => type;
        public StackZone Zone { get; private set; }


        private void Awake()
        {
            _placementBehavior = GetComponent<PlacementItem>();
        }


        public void SetZone(StackZone zone)
        {
            if (zone == Zone)
            {
                return;
            }

            Zone = zone;
        }
    }
}