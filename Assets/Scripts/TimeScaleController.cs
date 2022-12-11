using UnityEngine;

namespace ColonizationMobileGame
{
    public class TimeScaleController : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float timeScale;
        
        
        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                Time.timeScale = timeScale;
            }
        }
    }
}