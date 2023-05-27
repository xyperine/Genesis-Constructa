using DG.Tweening;
using UnityEngine;

namespace GenesisConstructa.UI.ItemsAmount.Panel
{
    public class ItemsAmountPanelAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        
        [SerializeField] private float duration;

        private Tween _upTween;
        private Tween _downTween;


        public void ScaleUp()
        {
            if (_upTween is {active: true})
            {
                return;
            }
            
            _downTween?.Kill();
            _upTween = StartUpTween();
        }


        private Tween StartUpTween()
        {
            return rectTransform.DOScale(1f, duration).SetEase(Ease.OutBack);
        }
        
        
        public void ScaleDown()
        {
            if (_downTween is {active: true})
            {
                return;
            }

            _upTween?.Kill();
            _downTween = StartDownTween();
        }
        
        
        private Tween StartDownTween()
        {
            return rectTransform.DOScale(0f, duration * 0.3f).SetEase(Ease.InCubic);
        }
    }
}