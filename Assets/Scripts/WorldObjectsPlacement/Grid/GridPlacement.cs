using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement.Grid
{
    public class GridPlacement : WorldPlacementArea<GridWorldPlacementSettings>
    {
        private Vector3 _area;
        private Vector3 _center;


        protected override Vector3 GetPositionForNewItem()
        {
            int emptySpotIndex = items.FirstNullIndex;
            Vector3 padding = placementSettings.DefaultItemSize * 0.5f;
            Vector3 position = padding;
            int[] fillingOrderAxes = new int[3];

            for (int i = 0; i < 3; i++)
            {
                fillingOrderAxes[i] = (int) placementSettings.FillingOrder[i];
            }

            float firstAxisRawPosition = emptySpotIndex % placementSettings.Size[fillingOrderAxes[0]];
            float secondAxisRawPosition = Mathf.Floor(emptySpotIndex / placementSettings.Size[fillingOrderAxes[0]])
                                          % placementSettings.Size[fillingOrderAxes[1]];
            float thirdAxisRawPosition = Mathf.Floor(emptySpotIndex / (placementSettings.Size[fillingOrderAxes[0]]
                                                                       * placementSettings.Size[fillingOrderAxes[1]]))
                                         % placementSettings.Size[fillingOrderAxes[2]];

            float[] axesRawPositions =
            {
                firstAxisRawPosition,
                secondAxisRawPosition,
                thirdAxisRawPosition,
            };

            for (int i = 0; i < 3; i++)
            {
                int axisIndex = fillingOrderAxes[i];
                position[axisIndex] = padding[axisIndex] + placementSettings.DefaultItemSize[axisIndex] * axesRawPositions[i];
            }

            return position;
        }


        private void OnValidate()
        {
            RecalculateBounds();
        }


        private void RecalculateBounds()
        {
            float width = placementSettings.Width * placementSettings.DefaultItemSize.x;
            float height = placementSettings.Height * placementSettings.DefaultItemSize.y;
            float depth = placementSettings.Depth * placementSettings.DefaultItemSize.z;
            
            _area = new Vector3(width, height, depth);
            _center = _area * 0.5f;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(_center, _area);
        }
    }
}