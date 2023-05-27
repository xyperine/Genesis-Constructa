using GenesisConstructa.ItemsExtraction.ConditionsLogic;
using UnityEngine;

namespace GenesisConstructa.ItemsExtraction.Extra.KeyItemSystem
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