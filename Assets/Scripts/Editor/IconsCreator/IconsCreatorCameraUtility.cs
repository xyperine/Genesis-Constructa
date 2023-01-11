using ColonizationMobileGame.Utility.Extensions;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace IconsCreatorNS
{
    public class IconsCreatorCameraUtility
    {
        private const string ICONS_CREATION_CAMERA_TAG = "IconsCreationCamera";

        private Camera _camera;
        private GameObject _targetObject;

        private int _resolution;
        private float _distanceToTarget = 10f;
        private Texture2D _cameraViewTexture;

        private Vector3 CameraOffset => -_camera.transform.forward * _distanceToTarget;
        private float RenderTexturePixelToWorldUnits => _camera.orthographicSize / _resolution;
        
        public string IconsCreationCameraTag => ICONS_CREATION_CAMERA_TAG;
        public bool Orthographic => _camera.orthographic;


        public void RetrieveCamera()
        {
            if (_camera)
            {
                if (_camera.gameObject.CompareTag(ICONS_CREATION_CAMERA_TAG)) 
                {
                    return;
                }
            }
            
            GameObject cameraObject = GameObject.FindGameObjectWithTag(ICONS_CREATION_CAMERA_TAG);
            if (!cameraObject)
            {
                Debug.LogWarning($"No game object tagged \"{ICONS_CREATION_CAMERA_TAG}\" found!");
                return;
            }
            
            _camera = cameraObject.GetComponent<Camera>();

            if (!_camera)
            {
                Debug.LogWarning($"No camera found! Create camera object and tag it \"{ICONS_CREATION_CAMERA_TAG}\"");
            }
            
            Debug.Log("Camera found!");
        }


        public void SetData(GameObject targetObject, int resolution)
        {
            _targetObject = targetObject;
            _resolution = resolution;
        }


        public void PositionCamera()
        {
            Transform cameraTransform = _camera.transform;
            if (!_targetObject)
            {
                Debug.LogWarning("No target found!");
                return;
            }

            cameraTransform.rotation = Quaternion.AngleAxis(45, Vector3.right);
            
            Bounds targetBounds = _targetObject.GetGameObjectBounds();
            
            _distanceToTarget = targetBounds.size.z / 2 + 10;
            Vector3 targetCenter = targetBounds.center;
            cameraTransform.position = targetCenter + CameraOffset;

            _camera.orthographicSize = targetBounds.size.BiggestComponentValue() * 0.5f;
        }


        public void SetCameraSize()
        {
            float lengthBounds = _targetObject.GetGameObjectBounds().size.BiggestComponentValue() * 0.5f;
            _camera.orthographicSize = lengthBounds;
        }
        

        public Texture2D CaptureCameraView()
        {
            SetCameraTargetTexture();
            RenderTexture.active = _camera.targetTexture;

            _camera.Render();

            Texture2D image = new Texture2D(_resolution, _resolution);
            image.ReadPixels(new Rect(0, 0, _resolution, _resolution), 0, 0);
            image.Apply();

            _camera.targetTexture = null;
            RenderTexture.active = null;

            return image;
        }


        private void SetCameraTargetTexture()
        {
            _camera.targetTexture = RenderTexture.GetTemporary(_resolution, _resolution);
        }


        public void CenterCamera()
        {
            _cameraViewTexture = CaptureCameraView();

            Transform cameraTransform = _camera.transform;
            
            Vector2 contentCenter = _cameraViewTexture.NonTransparentContentCenter();
            
            Vector2 difference = Vector2.one * (_resolution * 0.5f) - contentCenter;
            Vector2 differenceWorldUnits = difference * RenderTexturePixelToWorldUnits;
            Vector3 differenceAlongTheCameraPlane = cameraTransform.rotation *differenceWorldUnits;

            Vector3 targetCenter = _targetObject.GetGameObjectBounds().center - differenceAlongTheCameraPlane;
            cameraTransform.position = targetCenter + CameraOffset;
        }
    }
}