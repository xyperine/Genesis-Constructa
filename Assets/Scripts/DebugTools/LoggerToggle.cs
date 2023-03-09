using UnityEngine;

namespace ColonizationMobileGame.DebugTools
{
    public class LoggerToggle : MonoBehaviour
    {
        [SerializeField] private bool activeInEditor;
        [SerializeField] private bool activeInBuild;


        private void Awake()
        {
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = activeInEditor;
#else
            Debug.unityLogger.logEnabled = activeInBuild;
#endif
        }
    }
}