using ColonizationMobileGame.UI.ArrowPointers.Target;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public class ArrowPointer : MonoBehaviour
    {
        [SerializeField, Range(0.5f, 10f)] private float animationSpeed = 3f;
        [SerializeField, Range(0f, 50f)] private float minYOffset;
        [SerializeField, Range(50f, 100f)] private float maxYOffset;
        
        private Camera _camera;

        public bool Free => Target is not {Valid: true};
        public ArrowPointerTarget Target { get; private set; }


        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }


        private void Update()
        {
            if (Target.OnScreen)
            {
                OnScreen();
            }
            else
            {
                OffScreen();
            }
        }


        private void OnScreen()
        {
            Vector3 targetScreenPosition = _camera.WorldToScreenPoint(Target.Position);

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
            Vector3 targetPositionInScreenCoords = _camera.WorldToScreenPoint(Target.Position);
            
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


        public void PointTo(ArrowPointerTarget target)
        {
            TrySetTarget(target);

            if (Target == null)
            {
                return;
            }
            
            Target.Invalidated += Disable;
            
            gameObject.SetActive(true);
        }


        private void TrySetTarget(ArrowPointerTarget target)
        {
            if (target == null)
            {
                return;
            }
            
            Target ??= target;
        }


        private void Disable()
        {
            gameObject.SetActive(false);
            Target = null;
        }
    }
}