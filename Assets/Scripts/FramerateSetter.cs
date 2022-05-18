using UnityEngine;

namespace MoonPioneerClone
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
