using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS
{
    public abstract class AreaVisualizationTargetFitter
    {
        private readonly AreaVisualizationSettingsData _settings;
        protected readonly Rectangle areaRectangle;

        protected Vector2 areaSize;

        protected abstract Vector2 AreaSize { get; }


        protected AreaVisualizationTargetFitter(Rectangle areaRectangle, AreaVisualizationSettingsData settings)
        {
            this.areaRectangle = areaRectangle;
            _settings = settings;
        }


        public void Fit(Transform visualizerTransform)
        {
            areaSize = AreaSize;

            SetPosition(visualizerTransform);
            
            SetSize();
        }


        protected abstract void SetPosition(Transform transform);


        private void SetSize()
        {
            float padding = 1 + _settings.ProportionalPadding;
            
            areaRectangle.Width = areaSize.x * padding;
            areaRectangle.Height = areaSize.y * padding;

            areaRectangle.CornerRadius = Mathf.Min(areaSize.x, areaSize.y) * _settings.CornerRadius;
        }
    }
}