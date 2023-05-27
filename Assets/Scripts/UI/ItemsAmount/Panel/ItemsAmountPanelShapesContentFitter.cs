using System.Linq;
using DG.Tweening;
using GenesisConstructa.Utility.Extensions;
using Shapes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GenesisConstructa.UI.ItemsAmount.Panel
{
    public class ItemsAmountPanelShapesContentFitter : MonoBehaviour
    {
        [SerializeField] private Rectangle rectangle;
        [SerializeField, ShowIf(nameof(isCameraSpace))] private VerticalLayoutGroup layoutGroup;

        [SerializeField] private float duration;
        [SerializeField] private float entryHeight;
        [SerializeField] private float spacing;

        [SerializeField, HideInInspector] private bool isCameraSpace;
        
        private Tween _shrinkingTween;


        private void OnValidate()
        {
            RetrieveCanvasRenderMode();

            if (isCameraSpace)
            {
                layoutGroup = GetComponent<VerticalLayoutGroup>();
            }
        }


        private void RetrieveCanvasRenderMode()
        {
            Canvas canvas = GetComponentInParent<Canvas>(true);
            if (!canvas)
            {
                Debug.LogWarning("This component requires Canvas component up in the hierarchy to work properly!");
                return;
            }
            
            isCameraSpace = canvas.renderMode == RenderMode.ScreenSpaceCamera;
        }


        private void Awake()
        {
            SetPosition();
        }


        private void SetPosition()
        {
            float y = isCameraSpace ? layoutGroup.padding.top : entryHeight;
            RectTransform rectangleTransform = rectangle.GetComponent<RectTransform>();
            Vector3 newPosition = rectangleTransform.anchoredPosition3D.WithY(-y);
            
            rectangleTransform.anchoredPosition3D = newPosition;
        }


        public void ResizeBackground()
        {
            float newHeight = CalculateHeight();
            SetHeight(newHeight);
        }


        private float CalculateHeight()
        {
            RectTransform[] activeChildren =
                transform.Cast<RectTransform>().Where(t => t.gameObject.activeInHierarchy).ToArray();

            float activeChildrenTotalHeight = activeChildren.Sum(c => c.rect.height);
            float finalHeight = activeChildrenTotalHeight + Mathf.Max(0, activeChildren.Length - 1) * spacing;

            return finalHeight;
        }
        

        private void SetHeight(float newHeight)
        {
            _shrinkingTween?.Kill(true);

            if (newHeight >= rectangle.Height)
            {
                rectangle.Height = newHeight;
                return;
            }

            _shrinkingTween = DOTween.To(() => rectangle.Height, h => rectangle.Height = h, newHeight, duration);
        }


        public void SetVisibility(bool visible)
        {
            rectangle.enabled = visible;
        }
    }
}