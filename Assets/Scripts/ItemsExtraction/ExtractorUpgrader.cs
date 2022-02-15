using MoonPioneerClone.ItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public class ExtractorUpgrader : MonoBehaviour
    {
        [SerializeField] private ItemsConsumer consumer;

        [SerializeField] private ExtractorProductionUnit productionUnit;
        [SerializeField] private StackZone productionZone;


        private void Awake()
        {
            consumer.BlockSatisfied += Upgrade;
        }


        private void Upgrade()
        {
            productionUnit.IncreaseProductionRate();
            productionZone.Upgrade();
        }
    }
}