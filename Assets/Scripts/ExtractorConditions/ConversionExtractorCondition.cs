using MoonPioneerClone.ItemsExtraction;
using MoonPioneerClone.ItemsExtraction.ConditionsLogic;
using UnityEngine;

namespace MoonPioneerClone.ExtractorConditions
{
    public sealed class ConversionExtractorCondition : ExtractorCondition
    {
        [SerializeField] private ExtractorConversionUnit conversionUnit;


        public override bool Met()
        {
            return conversionUnit.Active;
        }
    }
}