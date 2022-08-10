using UnityEngine;

namespace ColonizationMobileGame.Utility
{
    public class FramerateSetter : MonoBehaviour
    {
#if !UNITY_EDITOR
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
#endif
    }
}
