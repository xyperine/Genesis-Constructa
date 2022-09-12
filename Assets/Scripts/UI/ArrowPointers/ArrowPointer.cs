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
        private ArrowPointerTarget _target;

        public bool Free => _target is not {Valid: true};
        public ArrowPointerTarget Target => _target;


        public void SetCamera(Camera camera)
        {
            _camera = camera;
        }


        private void Update()
        {
            if (_target.OnScreen)
            {
                OnScreen();
            }
            else
            {
                OffScreen();
            }
        }


        private Vector3 ClampToScreen(Vector3 position)
        {
            float screenX = Screen.width;
            float screenY = Screen.height;
            const float offset = 60;
            
            Vector3 positionOnScreen = Vector3.zero;
            positionOnScreen.x = Mathf.Clamp(position.x, 0f + offset, screenX - offset);
            positionOnScreen.y = Mathf.Clamp(position.y, 0f + offset, screenY - offset);

            return positionOnScreen;
        }


        private void OnScreen()
        {
            image.sprite = onScreenArrow;

            Vector3 targetScreenPosition = _camera.WorldToScreenPoint(_target.Position);
            transform.position = targetScreenPosition + Vector3.up * 50f;
            transform.localEulerAngles = Vector3.zero;
        }


        private void OffScreen()
        {
            image.sprite = offScreenArrow;
            
            Vector3 targetScreenPosition = _camera.WorldToScreenPoint(_target.Position);
            Vector3 centerOfTheScreen = new Vector3(Screen.width, Screen.height, 0f) / 2f;
            Vector3 direction = (targetScreenPosition - centerOfTheScreen).normalized;
            transform.localEulerAngles =
                Vector3.forward * (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f);
            
            Vector3 positionOnScreen = ClampToScreen(targetScreenPosition);
            transform.position = positionOnScreen;
        }


        public void Draw(ArrowPointerTarget target)
        {
            TrySetTarget(target);

            if (_target == null)
            {
                return;
            }
            
            _target.Invalidated += Disable;
            
            gameObject.SetActive(true);
        }


        private void TrySetTarget(ArrowPointerTarget target)
        {
            if (target == null)
            {
                return;
            }
            
            _target ??= target;
            
            if (_target.Valid)
            {
                return;
            }
            
            _target = target;
        }


        private void Disable()
        {
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }
}