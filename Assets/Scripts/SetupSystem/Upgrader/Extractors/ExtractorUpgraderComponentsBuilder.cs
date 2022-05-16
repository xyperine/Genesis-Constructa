﻿using System;
using MoonPioneerClone.ItemsExtraction.Upgrading;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.Upgrader.Extractors
{
    [Serializable]
    public class ExtractorUpgraderComponentsBuilder : UpgraderComponentsBuilder<ExtractorUpgraderSetupData>
    {
        [SerializeField] private ExtractorUpgrader upgrader;

        
        public override void SetupCollider()
        {
            collider.radius = setupData.ColliderRadius;
        }


        protected override void SetupItemsConsumer()
        {
            consumer.Setup(setupData.Chain.RequirementsChain);
        }


        protected override void SetupUpgrader()
        {
            upgrader.Construct(setupData);
        }
    }
}