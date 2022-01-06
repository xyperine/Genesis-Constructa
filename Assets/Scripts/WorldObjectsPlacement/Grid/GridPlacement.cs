using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Grid
{
    public sealed class GridPlacement : WorldPlacementArea<GridWorldPlacementSettings>
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
            itemsKeepingBehaviour = new GridItemsKeepingBehaviour(placementSettings.MaxItems);
        }


        protected override Vector3 GetPositionForNewItem()
        {
            Vector3 axesRawPositions = CalculateAxesRawPositions();
            Vector3 position = CalculatePosition(axesRawPositions);

            return position;
        }
        

        private Vector3 CalculateAxesRawPositions()
        {
            int emptySpotIndex = itemsKeepingBehaviour.FirstNullIndex;
            
            float firstAxisRawPosition = emptySpotIndex % placementSettings.Size[_fillingOrderAxes[0]];
            float secondAxisRawPosition = Mathf.Floor(emptySpotIndex / placementSettings.Size[_fillingOrderAxes[0]])
                                          % placementSettings.Size[_fillingOrderAxes[1]];
            float thirdAxisRawPosition = Mathf.Floor(emptySpotIndex / (placementSettings.Size[_fillingOrderAxes[0]]
                                                                       * placementSettings.Size[_fillingOrderAxes[1]]))
                                         % placementSettings.Size[_fillingOrderAxes[2]];

            return new Vector3(firstAxisRawPosition, secondAxisRawPosition, thirdAxisRawPosition);
        }


        private Vector3 CalculatePosition(Vector3 axesRawPositions)
        {
            Vector3 padding = placementSettings.DefaultItemSize * 0.5f;
            Vector3 position = padding;
            
            for (int i = 0; i < 3; i++)
            {
                int axisIndex = _fillingOrderAxes[i];
                position[axisIndex] = padding[axisIndex] + placementSettings.DefaultItemSize[axisIndex] * axesRawPositions[i];
            }

            return position;
        }
    }
}