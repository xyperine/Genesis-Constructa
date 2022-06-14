using ColonizationMobileGame.ItemsExtraction.ConditionsLogic;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra
{
    public sealed class SpaceInStackZoneExtractorCondition : ExtractorCondition
    {
        [SerializeField] private StackZone productionStackZone;


        public override bool Met()
        {
            return productionStackZone.CanTakeMore;
        }
    }
}