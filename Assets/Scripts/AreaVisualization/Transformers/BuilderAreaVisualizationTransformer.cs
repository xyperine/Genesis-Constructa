using ColonizationMobileGame.BuildSystem;
using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS.Transformers
{
    public class BuilderAreaVisualizationTransformer : AreaVisualizationTransformer
    {
        private readonly Builder _builder;
        
        
        public BuilderAreaVisualizationTransformer(Rectangle areaRectangle, AreaVisualizationSettingsData settings,
            Builder builder) : base(areaRectangle, settings)
        {
            _builder = builder;
        }


        public override void PerformTransformations(Transform transform)
        {
            Vector2 areaSize = _builder.GetStructureArea();

            if (areaSize.x > areaSize.y)
            {
                return;
            }

            Vector3 localRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, 90f);
        }


        public override void SetSize()
        {
            Vector2 areaSize = _builder.GetStructureArea();

            areaRectangle.Height = areaSize.x;
            areaRectangle.Width = areaSize.y;

            areaRectangle.CornerRadius = Mathf.Min(areaSize.x, areaSize.y) * settings.CornerRadius;
        }
    }
}