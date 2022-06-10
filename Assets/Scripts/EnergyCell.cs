using System.Collections;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.Utility;
using UnityEngine;

namespace ColonizationMobileGame
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