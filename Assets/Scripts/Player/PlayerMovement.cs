using UnityEngine;

namespace MoonPioneerClone.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private float speed;
        [SerializeField, Range(0f, 10f)] private float smoothness;

        private const float SMOOTHNESS_MODIFIER = 1f / 8f;
        
        private Vector3 _velocity;

        public float RelativeVelocity => Mathf.Clamp01(_velocity.magnitude / Time.fixedDeltaTime / speed);


        private void Update()
        {
            GetMovementThisFrame();
        }


        private void FixedUpdate()
        {
            if (_velocity == Vector3.zero)
            {
                return;
            }
            
            MoveAndRotate();
        }


        private void GetMovementThisFrame()
        {
            Vector3 input = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
            Vector3 newMovement = speed * Time.fixedDeltaTime * input;
            float t = 1f / (1f + smoothness) * SMOOTHNESS_MODIFIER;
            
            _velocity = Vector3.Lerp(_velocity, newMovement, t);
        }


        private void MoveAndRotate()
        {
            Vector3 newPosition = transform.position + _velocity;
            Quaternion newRotation = Quaternion.LookRotation(-_velocity);
            transform.SetPositionAndRotation(newPosition, newRotation);
        }
    }
}
