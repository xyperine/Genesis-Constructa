using UnityEngine;

namespace GenesisConstructa.GameFading
{
    public class AudioController : MonoBehaviour
    {
        public void SetVolume(float volume)
        {
            AudioListener.volume = volume;
        }
    }
}