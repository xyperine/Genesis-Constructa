using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacement.Core.Area
{
    public sealed class PlacementItemPositionCalculator
    {
        private readonly int[] _fillingOrderAxes = new int[3];
        private readonly Vector3 _alignedItemSize;
        
        private readonly PlacementAreaUpgradeableProperties _upgradeableProperties;

        private readonly bool _mirror;


        public PlacementItemPositionCalculator(PlacementAreaSettingsSO placementSettings, PlacementAreaUpgradeableProperties upgradeableProperties, bool mirror)
        {
            _alignedItemSize = placementSettings.AlignedItemSize;
            
            for (int i = 0; i < 3; i++)
            {
                _fillingOrderAxes[i] = (int) placementSettings.FillingOrder[i];
            }

            _upgradeableProperties = upgradeableProperties;

            _mirror = mirror;
        }


        public Vector3 Calculate(int index)
        {
            Vector3 axesRawPositions = CalculateAxesRawPositions(index);
            Vector3 position = CalculateFinalPosition(axesRawPositions);

            return position;
        }
        
        
        private Vector3 CalculateAxesRawPositions(int index)
        {
            Vector3 areaSize = _upgradeableProperties.AreaSize;

            float firstAxisRawPosition = index % areaSize[_fillingOrderAxes[0]];
            float secondAxisRawPosition = Mathf.Floor(index / areaSize[_fillingOrderAxes[0]])
                                          % areaSize[_fillingOrderAxes[1]];
            float thirdAxisRawPosition = Mathf.Floor(index / (areaSize[_fillingOrderAxes[0]]
                                                                       * areaSize[_fillingOrderAxes[1]]))
                                         % areaSize[_fillingOrderAxes[2]];

            if (_mirror)
            {
                firstAxisRawPosition = areaSize[_fillingOrderAxes[0]] - firstAxisRawPosition - 1;
                secondAxisRawPosition = areaSize[_fillingOrderAxes[1]] - secondAxisRawPosition - 1;
            }
            
            return new Vector3(firstAxisRawPosition, secondAxisRawPosition, thirdAxisRawPosition);
        }


        private Vector3 CalculateFinalPosition(Vector3 axesRawPositions)
        {
            Vector3 padding = _alignedItemSize * 0.5f;
            Vector3 position = padding;
            
            for (int i = 0; i < 3; i++)
            {
                int axisIndex = _fillingOrderAxes[i];
                position[axisIndex] = padding[axisIndex] + _alignedItemSize[axisIndex] * axesRawPositions[i];
            }

            return position;
        }
    }
}