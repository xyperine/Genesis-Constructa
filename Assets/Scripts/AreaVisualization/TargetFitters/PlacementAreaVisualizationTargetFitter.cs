using ColonizationMobileGame.ItemsPlacement.Core.Area;
using ColonizationMobileGame.Utility.Extensions;
using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS.TargetFitters
{
    public class PlacementAreaVisualizationTargetFitter : AreaVisualizationTargetFitter
    {
        private readonly PlacementArea _placementArea;

        protected override bool RotationCondition => areaSize.x <= areaSize.y;
        protected override Vector2 AreaSize => _placementArea.GetUpgradeableData().MirroredAreaDimensions.XZPlaneVector2();


        public PlacementAreaVisualizationTargetFitter(Rectangle areaRectangle, AreaVisualizationSettingsData settings,
            PlacementArea placementArea) : base(areaRectangle, settings)
        {
            _placementArea = placementArea;
        }


        protected override void PerformTransformations(Transform transform)
        {
            base.PerformTransformations(transform);
            
            transform.localPosition = areaSize.XZPlaneToVector3() * 0.5f;
        }
    }
}