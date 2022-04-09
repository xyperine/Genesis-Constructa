using System.Collections;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions
{
    public class EnergyCellSlot : StackZone
    {
        [SerializeField, Range(5f, 60f)] private float cellLifetime;


        public override void Add(StackZoneItem item)
        {
            base.Add(item);

            StartCoroutine(ItemDestructionCoroutine(item));
        }


        private IEnumerator ItemDestructionCoroutine(StackZoneItem item)
        {
            yield return new WaitForSeconds(cellLifetime);

            Remove(item);
            Destroy(item.gameObject);
        }
    }
}