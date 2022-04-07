using System.Collections.Generic;
using MoonPioneerClone.ItemsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.PickUp
{
    public class PickUpStackZoneBehaviour : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private StackZone stackZone;
        [Header("Pick Up Settings")]
        [SerializeField] private List<StackZone> pickUpFrom = new List<StackZone>();

        public bool CanPickUpFrom(StackZone from) => pickUpFrom.Contains(from);


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