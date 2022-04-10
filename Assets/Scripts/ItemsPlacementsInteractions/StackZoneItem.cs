using MoonPioneerClone.ItemsPlacement.Core;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions
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
            Zone = zone;
        }


        public void SetFree()
        {
            if (Zone)
            {
                Zone.Remove(this);
            }
        }
    }
}