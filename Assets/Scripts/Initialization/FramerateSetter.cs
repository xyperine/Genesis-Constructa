using UnityEngine;

namespace ColonizationMobileGame.Initialization
{
    public class FramerateSetter : MonoBehaviour
    {
        [SerializeField, Range(0, 60)] private int targetFramerate = 60;
        
        
#if !UNITY_EDITOR
        private void Awake()
        {
            Application.targetFrameRate = targetFramerate;
        }
#endif
    }
}
