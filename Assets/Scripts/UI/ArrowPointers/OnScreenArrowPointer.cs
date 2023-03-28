using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public class OnScreenArrowPointer : MonoBehaviour
    {
        [SerializeField] private Transform pointerImageTransform;
        
        [SerializeField, Range(0.5f, 10f)] private float animationSpeed = 3f;
        [SerializeField, Range(0f, 50f)] private float minYOffset;
        [SerializeField, Range(50f, 100f)] private float maxYOffset;


        public void PointTo(Transform targetTransform)
        {
            transform.SetParent(targetTransform);
            transform.position = targetTransform.position;
            
            float yOffset = GetVerticalOffset();
            pointerImageTransform.localPosition = targetTransform.position + Vector3.up * yOffset;
            
            pointerImageTransform.localEulerAngles = Vector3.zero;
        }


        private float GetVerticalOffset()
        {
            float t = Mathf.Sin(Time.time * animationSpeed);
            t += 1;
            t /= 2f;
            
            float yOffset = Mathf.Lerp(minYOffset, maxYOffset, t);

            return yOffset;
        }
    }
}