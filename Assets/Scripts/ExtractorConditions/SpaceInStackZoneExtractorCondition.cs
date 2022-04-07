﻿using MoonPioneerClone.ItemsExtraction.ConditionsLogic;
using MoonPioneerClone.ItemsInteractions.StackZoneLogic;
using UnityEngine;

namespace MoonPioneerClone.ExtractorConditions
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