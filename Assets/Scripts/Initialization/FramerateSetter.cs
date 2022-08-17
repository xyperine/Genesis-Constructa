using UnityEngine;

namespace ColonizationMobileGame.Initialization
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
