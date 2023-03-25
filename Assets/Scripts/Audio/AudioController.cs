using UnityEngine;

namespace ColonizationMobileGame.Audio
{
    public class AudioController : MonoBehaviour
    {
        private void LateUpdate()
        {
            AudioListener.volume = Time.timeScale;
        }
    }
}