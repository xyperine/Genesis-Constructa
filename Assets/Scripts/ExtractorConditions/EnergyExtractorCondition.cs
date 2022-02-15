using MoonPioneerClone.ItemsExtraction.Conditions;
using MoonPioneerClone.ItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone.ExtractorConditions
{
    public class EnergyExtractorCondition : ExtractorCondition
    {
        [SerializeField] private EnergyCellSlot cellSlot;


        public override bool Met()
        {
            return cellSlot.HasItems;
        }
    }
}