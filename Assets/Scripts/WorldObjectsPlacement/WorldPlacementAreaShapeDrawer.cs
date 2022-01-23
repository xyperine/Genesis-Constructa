using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public abstract class WorldPlacementAreaShapeDrawer<TPlacementSettings> : MonoBehaviour
        where TPlacementSettings : WorldPlacementSettingsSO
    {
        [SerializeField] protected TPlacementSettings placementSettings;
        [SerializeField] private bool alwaysDraw;


        private void OnValidate()
        {
            RecalculateShape();
        }


        protected abstract void RecalculateShape();


        private void OnDrawGizmosSelected()
        {
            if (!alwaysDraw)
            {
                DrawShape();
            }
        }


        protected abstract void DrawShape();


        private void OnDrawGizmos()
        {
            if (alwaysDraw)
            {
                DrawShape();
            }
        }
    }
}