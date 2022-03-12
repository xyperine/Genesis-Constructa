using MoonPioneerClone.ItemsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction.Upgrading
{
    public sealed class ProductionStackZoneUpgrader : MonoBehaviour
    {
        [SerializeField] private StackZone stackZone;


        public void Upgrade(int newMaxItemsValue)
        {
            stackZone.Upgrade(newMaxItemsValue);
        }
    }
}