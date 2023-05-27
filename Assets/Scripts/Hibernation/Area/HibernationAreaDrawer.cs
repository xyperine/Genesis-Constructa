using Shapes;
using UnityEngine;

namespace GenesisConstructa.Hibernation.Area
{
    public class HibernationAreaDrawer : MonoBehaviour
    {
        [SerializeField] private Rectangle rectangle;

        [SerializeField] private float cornerRadius;


        public void UpdateRectangle(float size)
        {
            rectangle.Width = size;
            rectangle.Height = size;

            rectangle.CornerRadius = size * cornerRadius;
        }
    }
}