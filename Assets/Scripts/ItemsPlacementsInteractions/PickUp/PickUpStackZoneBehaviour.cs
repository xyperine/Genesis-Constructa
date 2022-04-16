using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.PickUp
{
    public class PickUpStackZoneBehaviour : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private StackZone stackZone;


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