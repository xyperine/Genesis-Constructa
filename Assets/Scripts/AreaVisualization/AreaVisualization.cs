using ColonizationMobileGame.ItemsPlacement.Core.Area;
using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using ColonizationMobileGame.Utility;
using Shapes;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ColonizationMobileGame.AreaVisualizationNS
{
    [RequireComponent(typeof(Rectangle))]
    public class AreaVisualization : MonoBehaviour
    {
        [SerializeField] private AreaVisualizationSettingsSO settings;
        [SerializeField] private PlacementArea placementArea;
        [SerializeField] private Rectangle areaRectangle;

        private PlacementAreaUpgradeableProperties _upgradeableProperties;
        private float _padding;

        
#if UNITY_EDITOR
        private void OnValidate()
#else
        private void Awake()
#endif
        {
            ApplySettingsToRectangle();
        }


        private void ApplySettingsToRectangle()
        {
            areaRectangle.ZOffsetFactor = settings.DepthOffsetFactor;
            areaRectangle.Color = settings.Color;
        }


        private void Start()
        {
            SetData();
            SetPositionAndRotation();
        }


        private void SetData()
        {
            _padding = 1 + settings.ProportionalPadding;
            _upgradeableProperties = placementArea.GetUpgradeableData();
        }


        private void SetPositionAndRotation()
        {
            Vector2 areaSize = _upgradeableProperties.ScaledAreaSize.XZPlaneVector2();
            
            transform.localPosition = areaSize.XZPlaneToVector3() * 0.5f;
            
            if (areaSize.x <= areaSize.y)
            {
                return;
            }

            Vector3 localRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, 90f);
        }


        private void Update()
        {
            SetSize();
        }


        private void SetSize()
        {
            Vector2 areaSize = _upgradeableProperties.ScaledAreaSize.XZPlaneVector2();
            
            areaRectangle.Height = areaSize.x * _padding;
            areaRectangle.Width = areaSize.y * _padding;

            areaRectangle.CornerRadius = Mathf.Min(areaSize.x, areaSize.y) * settings.CornerRadius;
        }
    }
}