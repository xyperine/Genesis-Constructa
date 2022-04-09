using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.PickUp
{
    public class PickUpStackZoneBehaviour : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private StackZone stackZone;


        public void PickUpItem(StackZoneItem item)
        {
            StackZone itemZone = item.Zone;
            
            itemZone.Remove(item);
            Add(item);
        }


        public void Add(StackZoneItem item)
        {
            stackZone.Add(item);
        }
    }
}