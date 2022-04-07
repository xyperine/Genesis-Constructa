﻿using MoonPioneerClone.ItemsExtraction.ConditionsLogic;
using MoonPioneerClone.ItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone.ExtractorConditions
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