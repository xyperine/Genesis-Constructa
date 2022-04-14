using System.Collections;
using MoonPioneerClone.ItemsExtraction.ConditionsLogic;
using MoonPioneerClone.ItemsPlacementsInteractions;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Target;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction
{
    public sealed class ExtractorConversionUnit : InteractionTarget
    {
        [SerializeField] private ItemType[] acceptableItems;
        [SerializeField] private ExtractorProductionRateSO productionRate;
        [SerializeField] private StackZone productionStackZone;
        [SerializeField] private ExtractorConditionsSet workConditionsSet;
        
        private bool _canTakeMore = true;

        public override bool CanTakeMore => workConditionsSet.Met && _canTakeMore && productionStackZone.CanTakeMore;
        public override ItemType[] AcceptableItems => acceptableItems;

        public bool Active { get; private set; }


        public override void Add(StackZoneItem item)
        {
            StartCoroutine(ConversionCoroutine());

            Destroy(item.gameObject, 0.1f);
        }


        private IEnumerator ConversionCoroutine()
        {
            Active = true;
            _canTakeMore = false;

            yield return Helpers.GetWaitForSeconds(1f / productionRate.ItemsPerSecond);

            Active = false;
            _canTakeMore = true;
        }
    }
}