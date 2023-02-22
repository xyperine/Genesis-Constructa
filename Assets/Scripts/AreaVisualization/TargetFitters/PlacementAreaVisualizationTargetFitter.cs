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

        protected override Vector2 AreaSize => _placementArea.GetUpgradeableData().MirroredAreaDimensions.XZPlaneVector2();


        public PlacementAreaVisualizationTargetFitter(Rectangle areaRectangle, AreaVisualizationSettingsData settings,
            PlacementArea placementArea) : base(areaRectangle, settings)
        {
            _placementArea = placementArea;
        }


        protected override void PerformTransformations(Transform transform)
        {
            transform.localPosition = areaSize.XZPlaneToVector3() * 0.5f;
            
            if (areaSize.x > areaSize.y)
            {
                return;
            }

            Vector3 localRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, 90f);
        }
    }
}