using ColonizationMobileGame.UI.ArrowPointers.Target;
using UnityEngine;
using UnityEngine.UI;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public class ArrowPointer : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite offScreenArrow;
        [SerializeField] private Sprite onScreenArrow;

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
            image.sprite = onScreenArrow;

            Vector3 targetScreenPosition = _camera.WorldToScreenPoint(Target.Position);
            transform.position = targetScreenPosition + Vector3.up * 50f;
            transform.localEulerAngles = Vector3.zero;
        }


        private void OffScreen()
        {
            image.sprite = offScreenArrow;
            
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
            Target = null;
            
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }
}