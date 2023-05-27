using UnityEngine;

namespace GenesisConstructa.GameFading
{
    public class TimeScaler : MonoBehaviour
    {
        private float _defaultFixedDeltaTime;


        private void Awake()
        {
            _defaultFixedDeltaTime = Time.fixedDeltaTime;
        }


        public void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = _defaultFixedDeltaTime * timeScale;
        }
    }
}