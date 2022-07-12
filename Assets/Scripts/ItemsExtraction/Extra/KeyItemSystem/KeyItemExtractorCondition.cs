using ColonizationMobileGame.ItemsExtraction.ConditionsLogic;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItemExtractorCondition : ExtractorCondition
    {
        [SerializeField] private KeyItemsSet keyItemsSet;

        public override bool Met()
        {
            return keyItemsSet.Filled;
        }
    }
}