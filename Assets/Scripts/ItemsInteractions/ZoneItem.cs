using MoonPioneerClone.WorldObjectsPlacement;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions
{
    [RequireComponent(typeof(WorldPlacementItem))]
    public class ZoneItem : MonoBehaviour
    {
        [SerializeField] private ResourceType type;

        private WorldPlacementItem _placementBehavior;

        public bool Moving => _placementBehavior.Moving;
        public ResourceType Type => type;
        public StackZone Zone { get; private set; }


        private void Awake()
        {
            _placementBehavior = GetComponent<WorldPlacementItem>();
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