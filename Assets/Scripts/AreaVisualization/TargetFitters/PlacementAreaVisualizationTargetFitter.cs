using GenesisConstructa.ItemsPlacement.Core.Area;
using GenesisConstructa.Utility.Extensions;
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


        protected override void SetPosition(Transform transform)
        {
            transform.localPosition = areaSize.XZPlaneToVector3() * 0.5f;
        }
    }
}