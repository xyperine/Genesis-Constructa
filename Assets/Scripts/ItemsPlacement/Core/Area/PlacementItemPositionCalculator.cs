using UnityEngine;

namespace MoonPioneerClone.ItemsPlacement.Core.Area
{
    public sealed class PlacementItemPositionCalculator
    {
        private readonly int[] _fillingOrderAxes = new int[3];
        private readonly PlacementAreaSettingsSO _placementSettings;


        public PlacementItemPositionCalculator(PlacementAreaSettingsSO placementSettings)
        {
            _placementSettings = placementSettings;
            
            GetFillingOrderAxes();
        }
        
        
        private void GetFillingOrderAxes()
        {
            for (int i = 0; i < 3; i++)
            {
                _fillingOrderAxes[i] = (int) _placementSettings.FillingOrder[i];
            }
        }
        
        
        public Vector3 Calculate(int index)
        {
            Vector3 axesRawPositions = CalculateAxesRawPositions(index);
            Vector3 position = CalculateFinalPosition(axesRawPositions);

            return position;
        }
        
        
        private Vector3 CalculateAxesRawPositions(int index)
        {
            float firstAxisRawPosition = index % _placementSettings.AreaSize[_fillingOrderAxes[0]];
            float secondAxisRawPosition = Mathf.Floor(index / _placementSettings.AreaSize[_fillingOrderAxes[0]])
                                          % _placementSettings.AreaSize[_fillingOrderAxes[1]];
            float thirdAxisRawPosition = Mathf.Floor(index / (_placementSettings.AreaSize[_fillingOrderAxes[0]]
                                                              * _placementSettings.AreaSize[_fillingOrderAxes[1]]))
                                         % _placementSettings.AreaSize[_fillingOrderAxes[2]];

            return new Vector3(firstAxisRawPosition, secondAxisRawPosition, thirdAxisRawPosition);
        }


        private Vector3 CalculateFinalPosition(Vector3 axesRawPositions)
        {
            Vector3 padding = _placementSettings.AlignedItemSize * 0.5f;
            Vector3 position = padding;
            
            for (int i = 0; i < 3; i++)
            {
                int axisIndex = _fillingOrderAxes[i];
                position[axisIndex] = padding[axisIndex] + _placementSettings.AlignedItemSize[axisIndex] * axesRawPositions[i];
            }

            return position;
        }
    }
}