using Shapes;
using UnityEngine;

namespace GenesisConstructa.Hibernation.Area
{
    public class HibernationAreaIndicator : MonoBehaviour
    {
        [SerializeField] private HibernationArea area;
        [SerializeField] private Disc fill;
        [SerializeField] private Disc background;


        private void Update()
        {
            UpdateFill(area.HibernationProgress);
        }


        private void UpdateFill(float progress)
        {
            bool show = !Mathf.Approximately(progress, 0f) && progress < 1f;
            fill.enabled = show;
            background.enabled = show;

            if (show)
            {
                fill.AngRadiansEnd = -Mathf.PI * 2f * progress;
            }
        }
    }
}