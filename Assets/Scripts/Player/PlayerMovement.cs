using System;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.Player
{
    public class PlayerMovement : MonoBehaviour, ISceneSaveable
    {
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Joystick joystick;
        [SerializeField] private float speed;
        [SerializeField, Range(0f, 10f)] private float smoothness;

        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        private const float SMOOTHNESS_MODIFIER = 1f / 8f;
        
        private Vector3 _velocity;

        public float RelativeVelocity => Mathf.Clamp01(_velocity.magnitude / Time.fixedDeltaTime / speed);

        public int LoadingOrder => 0;
        public PermanentGuid Guid => guid;


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
            Vector3 newMovement = speed * input;
            float t = 1f / (1f + smoothness) * SMOOTHNESS_MODIFIER;
            
            _velocity = Vector3.Lerp(_velocity, newMovement, t);
        }


        private void MoveAndRotate()
        {
            Quaternion newRotation = Quaternion.LookRotation(-_velocity);

            rigidBody.velocity = _velocity;
            rigidBody.MoveRotation(newRotation);
        }


        public object Save()
        {
            return new SaveData
            {
                Position = transform.position,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            transform.position = saveData.Position;
        }
        
        
        [Serializable]
        private struct SaveData
        {
            public Vector3 Position { get; set; }
        }
    }
}
