using UnityEngine;

namespace GenesisConstructa.UI.ArrowPointers
{
    public class ArrowPointer : MonoBehaviour
    {
        [SerializeField] private OnScreenArrowPointer onScreenArrowPointer;
        [SerializeField] private OffScreenArrowPointer offScreenArrowPointer;
        
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
            
            DrawBasedOnConditions();
            
            gameObject.SetActive(true);
        }
        
        
        private void Update()
        {
            DrawBasedOnConditions();
        }


        private void DrawBasedOnConditions()
        {
            if (!_target.RequiresPointing)
            {
                Disable();
                return;
            }
            
            if (IsOnScreen())
            {
                offScreenArrowPointer.gameObject.SetActive(false);
                onScreenArrowPointer.gameObject.SetActive(true);

                onScreenArrowPointer.PointTo(_target.Transform);
            }
            else
            {
                onScreenArrowPointer.gameObject.SetActive(false);
                offScreenArrowPointer.gameObject.SetActive(true);

                offScreenArrowPointer.PointTo(_target.Transform, _camera);
            }
        }


        private bool IsOnScreen()
        {
            Vector3 viewportPosition = _camera.WorldToViewportPoint(_target.Transform.position);
            bool onScreen = viewportPosition.x is > 0f and < 1f &&
                            viewportPosition.y is > 0f and < 1f;
            return onScreen;
        }


        public void Disable()
        {
            gameObject.SetActive(false);
            _target = null;
            onScreenArrowPointer.transform.SetParent(transform);
        }
    }
}