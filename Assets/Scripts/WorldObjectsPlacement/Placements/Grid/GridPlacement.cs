using MoonPioneerClone.WorldObjectsPlacement.Collections;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Placements.Grid
{
    public class GridPlacement : WorldPlacementArea<GridPlacementSettingsSO>
    {
        private readonly int[] _fillingOrderAxes = new int[3];

        public override bool CanFitMore => Count < placementSettings.MaxItems;


        private void OnValidate()
        {
            GetFillingOrderAxes();
        }
        
        
        private void GetFillingOrderAxes()
        {
            for (int i = 0; i < 3; i++)
            {
                _fillingOrderAxes[i] = (int) placementSettings.FillingOrder[i];
            }
        }


        protected override void InitializeItemsKeepingBehaviour()
        {
            itemsCollection = new DefaultPlacementItemsCollection(placementSettings.MaxItems);
        }


        protected override void MoveItemInside(WorldPlacementItem item)
        {
            base.MoveItemInside(item);

            item.Rotate();
        }


        protected override Vector3 CalculatePositionForNewItem()
        {
            return CalculatePositionForNewItem(itemsCollection.FirstNullIndex);
        }


        protected Vector3 CalculatePositionForNewItem(int index)
        {
            Vector3 axesRawPositions = CalculateAxesRawPositions(index);
            Vector3 position = CalculatePosition(axesRawPositions);

            return position;
        }


        private Vector3 CalculateAxesRawPositions(int index)
        {
            float firstAxisRawPosition = index % placementSettings.Size[_fillingOrderAxes[0]];
            float secondAxisRawPosition = Mathf.Floor(index / placementSettings.Size[_fillingOrderAxes[0]])
                                          % placementSettings.Size[_fillingOrderAxes[1]];
            float thirdAxisRawPosition = Mathf.Floor(index / (placementSettings.Size[_fillingOrderAxes[0]]
                                                              * placementSettings.Size[_fillingOrderAxes[1]]))
                                         % placementSettings.Size[_fillingOrderAxes[2]];

            return new Vector3(firstAxisRawPosition, secondAxisRawPosition, thirdAxisRawPosition);
        }


        private Vector3 CalculatePosition(Vector3 axesRawPositions)
        {
            Vector3 padding = placementSettings.ItemSize * 0.5f;
            Vector3 position = padding;
            
            for (int i = 0; i < 3; i++)
            {
                int axisIndex = _fillingOrderAxes[i];
                position[axisIndex] = padding[axisIndex] + placementSettings.ItemSize[axisIndex] * axesRawPositions[i];
            }

            return position;
        }
    }
}