using ColonizationMobileGame.ItemsPlacement.Core.Area;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using ColonizationMobileGame.Utility;
using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS.Transformers
{
    public class PlacementAreaVisualizationTransformer : AreaVisualizationTransformer
    {
        private readonly PlacementArea _placementArea;


        public PlacementAreaVisualizationTransformer(Rectangle areaRectangle, AreaVisualizationSettingsData settings,
            PlacementArea placementArea) : base(areaRectangle, settings)
        {
            _placementArea = placementArea;
        }


        public override void PerformTransformations(Transform transform)
        {
            Vector2 areaSize = _placementArea.GetUpgradeableData().ScaledAreaSize.XZPlaneVector2();
            
            transform.localPosition = areaSize.XZPlaneToVector3() * 0.5f;
            
            if (areaSize.x <= areaSize.y)
            {
                return;
            }

            Vector3 localRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, 90f);
        }


        public override void SetSize()
        {
            Vector2 areaSize = _placementArea.GetUpgradeableData().ScaledAreaSize.XZPlaneVector2();
            float padding = 1 + settings.ProportionalPadding;
            
            areaRectangle.Height = areaSize.x * padding;
            areaRectangle.Width = areaSize.y * padding;

            areaRectangle.CornerRadius = Mathf.Min(areaSize.x, areaSize.y) * settings.CornerRadius;
        }
    }
}