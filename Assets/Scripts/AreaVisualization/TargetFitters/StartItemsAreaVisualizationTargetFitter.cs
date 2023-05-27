using GenesisConstructa.Utility.Extensions;
using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS.TargetFitters
{
    public class StartItemsAreaVisualizationTargetFitter : AreaVisualizationTargetFitter
    {
        private readonly GameObject _itemsHolderObject;

        protected override Vector2 AreaSize => _itemsHolderObject.GetBounds().size.XZPlaneVector2();


        public StartItemsAreaVisualizationTargetFitter(Rectangle areaRectangle, AreaVisualizationSettingsData settings,
            GameObject itemsHolderObject) : base(areaRectangle, settings)
        {
            _itemsHolderObject = itemsHolderObject;
        }


        protected override void SetPosition(Transform transform)
        {
            transform.position = _itemsHolderObject.GetBounds().center.XZPlane();
        }
    }
}