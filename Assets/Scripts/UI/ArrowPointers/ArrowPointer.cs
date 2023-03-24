using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public class ArrowPointer : MonoBehaviour
    {
        [SerializeField, Range(0.5f, 10f)] private float animationSpeed = 3f;
        [SerializeField, Range(0f, 50f)] private float minYOffset;
        [SerializeField, Range(50f, 100f)] private float maxYOffset;
        
        private Camera _camera;
        private ArrowPointerTarget _target;

        public bool Free => _target == null;


        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }


        public bool IsAlreadyPointingTo(ArrowPointerTarget target)
        {
            return _target == target;
        }
        
        
        public void PointTo(ArrowPointerTarget target)
        {
            _target = target;
            gameObject.SetActive(true);
        }
        
        
        private void Update()
        {
            if (!_target.RequiresPointing)
            {
                Disable();
                return;
            }
            
            if (IsOnScreen())
            {
                OnScreen();
            }
            else
            {
                OffScreen();
            }
        }


        public void Disable()
        {
            _target = null;
            gameObject.SetActive(false);
        }


        private bool IsOnScreen()
        {
            Vector3 viewportPosition = _camera.WorldToViewportPoint(_target.Transform.position);
            bool onScreen = viewportPosition.x is > 0f and < 1f &&
                            viewportPosition.y is > 0f and < 1f;
            return onScreen;
        }


        private void OnScreen()
        {
            Vector3 targetScreenPosition = _camera.WorldToScreenPoint(_target.Transform.position);

            float yOffset = GetVerticalOffset();
            transform.position = targetScreenPosition + Vector3.up * yOffset;
            
            transform.localEulerAngles = Vector3.zero;
        }


        private float GetVerticalOffset()
        {
            float t = Mathf.Sin(Time.time * animationSpeed);
            t += 1;
            t /= 2f;
            
            float yOffset = Mathf.Lerp(minYOffset, maxYOffset, t);

            return yOffset;
        }


        private void OffScreen()
        {
            Vector3 targetPositionInScreenCoords = _camera.WorldToScreenPoint(_target.Transform.position);
            
            Vector3 positionOnScreen = ClampScreenPosition(targetPositionInScreenCoords);
            transform.position = positionOnScreen;
            
            Vector3 centerOfTheScreen = new Vector3(Screen.width, Screen.height, 0f) / 2f;
            Vector3 direction = (targetPositionInScreenCoords - centerOfTheScreen).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            transform.localEulerAngles = Vector3.forward * angle;
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