using ColonizationMobileGame.ItemsExtraction.ConditionsLogic;
using UnityEngine;

namespace ColonizationMobileGame.ExtractorConditions
{
    public sealed class EnergyExtractorCondition : ExtractorCondition
    {
        [SerializeField] private EnergyCellSlot cellSlot;


        public override bool Met()
        {
            return cellSlot.HasItems;
        }
    }
}