using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacementsInteractions.PickUp
{
    public class PickUpStackZoneBehaviour : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private StackZone stackZone;
        
        public bool EnoughSpace => stackZone.CanTakeMore;


        public void Setup(StackZone zone)
        {
            stackZone = zone;
        }


        public void PickUpItem(StackZoneItem item)
        {
            stackZone.Add(item);
        }
    }
}