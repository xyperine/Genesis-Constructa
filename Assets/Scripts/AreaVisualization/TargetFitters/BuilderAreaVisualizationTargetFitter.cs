﻿using ColonizationMobileGame.BuildSystem;
using ColonizationMobileGame.Utility.Extensions;
using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS.TargetFitters
{
    public class BuilderAreaVisualizationTargetFitter : AreaVisualizationTargetFitter
    {
        private readonly Builder _builder;

        protected override bool RotationCondition => false;
        protected override Vector2 AreaSize => _builder.GetStructureArea();


        public BuilderAreaVisualizationTargetFitter(Rectangle areaRectangle, AreaVisualizationSettingsData settings,
            Builder builder) : base(areaRectangle, settings)
        {
            _builder = builder;
        }


        protected override void PerformTransformations(Transform transform)
        {
            base.PerformTransformations(transform);
            
            transform.position = _builder.transform.position + _builder.GetStructureBounds().center.XZPlane();
        }
    }
}