using System.Collections;
using MoonPioneerClone.ItemsPlacementsInteractions;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone
{
    public class EnergyCell : MonoBehaviour
    {
        [SerializeField] private StackZoneItem item;
        [SerializeField, Range(5f, 60f)] private float cellLifetime;


        public void Activate()
        {
            StartCoroutine(ItemDestructionCoroutine());
        }
        
        
        private IEnumerator ItemDestructionCoroutine()
        {
            yield return Helpers.GetWaitForSeconds(cellLifetime);

            item.SetFree();
            Destroy(gameObject);
        }
    }
}