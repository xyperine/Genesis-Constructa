using GenesisConstructa.ItemsExtraction.ConditionsLogic;
using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using UnityEngine;

namespace GenesisConstructa.ItemsExtraction.Extra
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