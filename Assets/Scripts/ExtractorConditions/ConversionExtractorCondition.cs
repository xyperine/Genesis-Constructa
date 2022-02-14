using MoonPioneerClone.ItemsExtraction.Conditions;
using MoonPioneerClone.ItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone.ExtractorConditions
{
    public class ConversionExtractorCondition : ExtractorCondition
    {
        [SerializeField] private StackZone conversionStackZone;


        public override bool Met()
        {
            return conversionStackZone.HasItems;
        }
    }
}