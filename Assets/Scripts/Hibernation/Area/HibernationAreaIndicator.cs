using Shapes;
using UnityEngine;

namespace ColonizationMobileGame.Hibernation.Area
{
    public class HibernationAreaIndicator : MonoBehaviour
    {
        [SerializeField] private Disc fill;


        public void UpdateFill(float secondsInside, float secondsRequired)
        {
            float progress = Mathf.Clamp01(secondsInside / secondsRequired);

            gameObject.SetActive(!Mathf.Approximately(progress, 0f) && progress < 1f);
            fill.AngRadiansEnd = Mathf.PI * 2f * progress;
        }
    }
}