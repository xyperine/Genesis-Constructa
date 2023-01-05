using ColonizationMobileGame.ItemsPlacementsInteractions;
using ColonizationMobileGame.Utility.Extensions;
using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS.Transformers
{
    public class StartItemsAreaVisualizationTransformer : AreaVisualizationTransformer
    {
        private readonly Transform _itemsHolder;


        public StartItemsAreaVisualizationTransformer(Rectangle areaRectangle, AreaVisualizationSettingsData settings,
            Transform itemsHolder) : base(areaRectangle, settings)
        {
            _itemsHolder = itemsHolder;
        }


        public override void PerformTransformations(Transform transform)
        {
            Vector2 areaSize = GetAreaSize();

            if (areaSize.x <= areaSize.y)
            {
                return;
            }

            Vector3 localRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, 90f);
        }


        private Vector2 GetAreaSize()
        {
            return _itemsHolder.gameObject.GetGameObjectBounds().size.XZPlaneVector2();
        }


        public override void SetSize()
        {
            Vector2 areaSize = GetAreaSize();

            areaRectangle.Height = areaSize.x;
            areaRectangle.Width = areaSize.y;

            areaRectangle.CornerRadius = Mathf.Min(areaSize.x, areaSize.y) * settings.CornerRadius;
        }
    }
}