using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS
{
    public abstract class AreaVisualizationTargetFitter
    {
        private readonly Rectangle _areaRectangle;
        private readonly AreaVisualizationSettingsData _settings;

        protected Vector2 areaSize;
        
        protected abstract bool RotationCondition { get; }
        protected abstract Vector2 AreaSize { get; }


        protected AreaVisualizationTargetFitter(Rectangle areaRectangle, AreaVisualizationSettingsData settings)
        {
            _areaRectangle = areaRectangle;
            _settings = settings;
        }


        public void Fit(Transform visualizerTransform)
        {
            PerformTransformations(visualizerTransform);
            
            SetSize();
        }
        
        
        protected virtual void PerformTransformations(Transform transform)
        {
            areaSize = AreaSize;

            if (!RotationCondition)
            {
                return;
            }

            Vector3 localRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, 90f);
        }


        private void SetSize()
        {
            float padding = 1 + _settings.ProportionalPadding;
            
            _areaRectangle.Width = areaSize.x * padding;
            _areaRectangle.Height = areaSize.y * padding;

            _areaRectangle.CornerRadius = Mathf.Min(areaSize.x, areaSize.y) * _settings.CornerRadius;
        }
    }
}