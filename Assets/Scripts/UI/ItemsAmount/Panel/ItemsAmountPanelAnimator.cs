using System.Linq;
using DG.Tweening;
using Shapes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.UI.ItemsAmount.Panel
{
    public class ItemsAmountPanelAnimator : MonoBehaviour
    {
        // Temporary solution, I want to replace transform-based animation with Shapes
        private enum Mode
        {
            Scale,
            Shapes,
        }

        [SerializeField] private Mode mode = Mode.Scale;
        
        [SerializeField, ShowIf(nameof(ScaleMode))] private RectTransform rectTransform;
        [SerializeField, ShowIf(nameof(ShapesMode))] private Rectangle rectangle;
        
        [SerializeField] private float duration;
        [SerializeField] private float entryHeight;
        [SerializeField] private float spacing;

        private bool ScaleMode => mode == Mode.Scale;
        private bool ShapesMode => mode == Mode.Shapes;
        
        private Tween _upTween;
        private Tween _downTween;


        public void ResizeBackground()
        {
            if (ShapesMode)
            {
                GetShapesTween();
            }
        }
        

        public void ScaleUp()
        {
            if (_upTween is {active: true})
            {
                return;
            }
            
            _downTween?.Kill();
            _upTween = GetUpTween();
        }


        private Tween GetUpTween()
        {
            return ScaleMode ? rectTransform.DOScale(1f, duration).SetEase(Ease.OutBack) : null;
        }


        private Tween GetShapesTween()
        {
            int activeChildrenCount = transform.Cast<Transform>().Count(t => t.gameObject.activeInHierarchy);
            float finalHeight = activeChildrenCount * entryHeight + Mathf.Max(0, activeChildrenCount - 1) * spacing;
            return DOTween.To(() => rectangle.Height, h => rectangle.Height = h, finalHeight, duration);
        }


        public void ScaleDown()
        {
            if (_downTween is {active: true})
            {
                return;
            }

            _upTween?.Kill();
            _downTween = GetDownTween();
        }
        
        
        private Tween GetDownTween()
        {
            return ScaleMode ? rectTransform.DOScale(0f, duration * 0.3f).SetEase(Ease.InCubic) : null;
        }
    }
}