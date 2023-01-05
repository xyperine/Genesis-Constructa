using ColonizationMobileGame.Utility.Extensions;
using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS.TargetFitters
{
    public class StartItemsAreaVisualizationTargetFitter : AreaVisualizationTargetFitter
    {
        private readonly GameObject _itemsHolderObject;
        
        protected override bool RotationCondition => areaSize.x <= areaSize.y;
        protected override Vector2 AreaSize => _itemsHolderObject.GetGameObjectBounds().size.XZPlaneVector2();


        public StartItemsAreaVisualizationTargetFitter(Rectangle areaRectangle, AreaVisualizationSettingsData settings,
            GameObject itemsHolderObject) : base(areaRectangle, settings)
        {
            _itemsHolderObject = itemsHolderObject;
        }
    }
}