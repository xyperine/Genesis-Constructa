using System;
using ColonizationMobileGame.Hibernation;
using UnityEngine;

namespace ColonizationMobileGame
{
    public class TimeScaler : MonoBehaviour
    {
        [SerializeField] private HibernationAreaEventSO hibernationAreaEventSO;

        [SerializeField, Min(0f)] private float slowDownDurationInSeconds = 3f;
        [SerializeField, Min(0f)] private float speedUpDurationInSeconds = 1f;

        private float _defaultFixedDeltaTime;

        private bool _slowingDown;
        private bool _speedingUp;

        public event Action TimeStopped;


        private void Awake()
        {
            _defaultFixedDeltaTime = Time.fixedDeltaTime;
        }


        private void OnEnable()
        {
            hibernationAreaEventSO.Changed += OnHibernationStatusChanged;
        }


        private void OnHibernationStatusChanged(bool objectInside)
        {
            if (objectInside)
            {
                BeginSlowDown();
            }
            else
            {
                BeginSpeedUp();
            }
        }


        public void BeginSlowDown()
        {
            _speedingUp = false;
            _slowingDown = true;
        }


        public void BeginSpeedUp()
        {
            _slowingDown = false;
            _speedingUp = true;
        }


        public void StopTime()
        {
            Time.timeScale = 0f;
            TimeStopped?.Invoke();
        }


        private void Update()
        {
            if (_slowingDown)
            {
                SlowDown();

                return;
            }

            if (_speedingUp)
            {
                SpeedUp();
            }
        }


        private void SlowDown()
        {
            Time.timeScale = Mathf.Clamp01(Time.timeScale - Time.unscaledDeltaTime / slowDownDurationInSeconds);
            Time.fixedDeltaTime = _defaultFixedDeltaTime * Time.timeScale;

            if (Time.timeScale <= 0f)
            {
                _slowingDown = false;
                TimeStopped?.Invoke();
            }
        }


        private void SpeedUp()
        {
            Time.timeScale = Mathf.Clamp01(Time.timeScale + Time.unscaledDeltaTime / speedUpDurationInSeconds);
            Time.fixedDeltaTime = _defaultFixedDeltaTime * Time.timeScale;

            if (Time.timeScale >= 1f)
            {
                _speedingUp = false;
            }
        }


        private void OnDisable()
        {
            hibernationAreaEventSO.Changed -= OnHibernationStatusChanged;
        }
    }
}