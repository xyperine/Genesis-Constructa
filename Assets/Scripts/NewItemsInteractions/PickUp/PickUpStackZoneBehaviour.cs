using MoonPioneerClone.NewItemsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.PickUp
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