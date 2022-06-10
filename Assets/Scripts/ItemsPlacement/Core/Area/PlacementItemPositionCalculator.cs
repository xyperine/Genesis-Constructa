﻿using ColonizationMobileGame.ItemsPlacementsInteractions.StackZoneLogic.Upgrading;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacement.Core.Area
{
    public sealed class PlacementItemPositionCalculator
    {
        private readonly int[] _fillingOrderAxes = new int[3];
        private readonly Vector3 _alignedItemSize;
        
        private readonly PlacementAreaUpgradeableProperties _upgradeableProperties;


        public PlacementItemPositionCalculator(PlacementAreaSettingsSO placementSettings, PlacementAreaUpgradeableProperties upgradeableProperties)
        {
            _alignedItemSize = placementSettings.AlignedItemSize;
            
            for (int i = 0; i < 3; i++)
            {
                _fillingOrderAxes[i] = (int) placementSettings.FillingOrder[i];
            }

            _upgradeableProperties = upgradeableProperties;
        }


        public Vector3 Calculate(int index)
        {
            Vector3 axesRawPositions = CalculateAxesRawPositions(index);
            Vector3 position = CalculateFinalPosition(axesRawPositions);

            return position;
        }
        
        
        private Vector3 CalculateAxesRawPositions(int index)
        {
            float firstAxisRawPosition = index % _upgradeableProperties.AreaSize[_fillingOrderAxes[0]];
            float secondAxisRawPosition = Mathf.Floor(index / _upgradeableProperties.AreaSize[_fillingOrderAxes[0]])
                                          % _upgradeableProperties.AreaSize[_fillingOrderAxes[1]];
            float thirdAxisRawPosition = Mathf.Floor(index / (_upgradeableProperties.AreaSize[_fillingOrderAxes[0]]
                                                              * _upgradeableProperties.AreaSize[_fillingOrderAxes[1]]))
                                         % _upgradeableProperties.AreaSize[_fillingOrderAxes[2]];

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