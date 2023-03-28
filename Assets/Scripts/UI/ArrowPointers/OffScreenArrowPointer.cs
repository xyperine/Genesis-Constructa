using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public class OffScreenArrowPointer : MonoBehaviour
    {
        private RectTransform _rectTransform;


        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }


        public void PointTo(Transform targetTransform, Camera camera)
        {
            Vector3 screenPointPosition = camera.WorldToScreenPoint(targetTransform.position);
            
            Vector3 positionOnScreen = ClampScreenPosition(screenPointPosition);
            _rectTransform.anchoredPosition = positionOnScreen;
            
            Vector3 centerOfTheScreen = new Vector3(Screen.width, Screen.height, 0f) / 2f;
            Vector3 direction = (screenPointPosition - centerOfTheScreen).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            _rectTransform.localEulerAngles = Vector3.forward * angle;
        }


        private Vector3 ClampScreenPosition(Vector3 position)
        {
            const float offset = 60f;
            
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            position.x = Mathf.Clamp(position.x, 0f + offset, screenWidth - offset);
            position.y = Mathf.Clamp(position.y, 0f + offset, screenHeight - offset);

            return position;
        }
    }
}