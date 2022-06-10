using UnityEngine;

namespace ColonizationMobileGame.Utility
{
    public class FramerateSetter : MonoBehaviour
    {
        private void Awake()
        {
#if !UNITY_EDITOR
            Application.targetFrameRate = 60;
#endif
        }
    }
}
