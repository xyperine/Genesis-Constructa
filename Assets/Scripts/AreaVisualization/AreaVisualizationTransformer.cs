using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS
{
    public abstract class AreaVisualizationTransformer
    {
        protected readonly AreaVisualizationSettingsData settings;
        protected readonly Rectangle areaRectangle;


        protected AreaVisualizationTransformer(Rectangle areaRectangle, AreaVisualizationSettingsData settings)
        {
            this.settings = settings;
            this.areaRectangle = areaRectangle;
        }
        
        public abstract void PerformTransformations(Transform transform);
        public abstract void SetSize();
    }
}