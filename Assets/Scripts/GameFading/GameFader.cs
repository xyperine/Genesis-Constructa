using System;
using GenesisConstructa.GameEvents;
using UnityEngine;

namespace GenesisConstructa.GameFading
{
    public class GameFader : MonoBehaviour
    {
        [SerializeField] private HibernationAreaEventSO hibernationAreaEventSO;

        [SerializeField] private TimeScaler timeScaler;
        [SerializeField] private AudioController audioController;
        [SerializeField] private ImageFader imageFader;
        
        private float _fadeDuration;
        private FadeFlags _fadeFlags;

        private bool _fadingOut;
        private bool _fadingIn;
        
        private float _fadeFactor = 1f;

        public event Action FadedOut;
        
        
        private void OnEnable()
        {
            hibernationAreaEventSO.Changed += OnHibernationStatusChanged;
        }


        private void OnHibernationStatusChanged(bool objectInside)
        {
            if (objectInside)
            {
                BeginFadeOut(3f, FadeFlags.All);
            }
            else
            {
                BeginFadeIn(1f, FadeFlags.All);
            }
        }
        

        public void BeginFadeIn(float duration, FadeFlags flags)
        {
            _fadeFlags = flags;
            _fadeDuration = duration;
            
            _fadingOut = false;
            _fadingIn = true;
        }


        public void BeginFadeOut(float duration, FadeFlags flags)
        {
            _fadeFlags = flags;
            _fadeDuration = duration;
            
            _fadingIn = false;
            _fadingOut = true;
        }


        public void FadeOutImmediately(FadeFlags flags)
        {
            BeginFadeOut(0.00001f, flags);
        }


        private void Update()
        {
            if (_fadingOut)
            {
                FadeOut();
                UpdateFaders();
            }

            if (_fadingIn)
            {
                FadeIn();
                UpdateFaders();
            }
        }


        private void FadeOut()
        {
            _fadeFactor = Mathf.Clamp01(_fadeFactor - Time.unscaledDeltaTime / _fadeDuration);

            if (_fadeFactor <= 0f)
            {
                _fadingOut = false;
                FadedOut?.Invoke();
            }
        }


        private void FadeIn()
        {
            _fadeFactor = Mathf.Clamp01(_fadeFactor + Time.unscaledDeltaTime / _fadeDuration);

            if (_fadeFactor >= 1f)
            {
                _fadingIn = false;
            }
        }


        private void UpdateFaders()
        {
            if (_fadeFlags.HasFlag(FadeFlags.Time))
            {
                timeScaler.SetTimeScale(_fadeFactor);
            }
            
            if (_fadeFlags.HasFlag(FadeFlags.Audio))
            {
                audioController.SetVolume(_fadeFactor);
            }
            
            if (_fadeFlags.HasFlag(FadeFlags.Visuals))
            {
                imageFader.SetAlpha(1f - _fadeFactor);
            }
        }
        
        
        private void OnDisable()
        {
            hibernationAreaEventSO.Changed -= OnHibernationStatusChanged;
        }
    }
}