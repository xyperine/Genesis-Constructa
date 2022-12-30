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

        private float _smoothnessT;
        
        private Vector3 _velocity;
        private Vector3 _newVelocity;

        public float RelativeVelocity => Mathf.Clamp01(_velocity.magnitude / Time.fixedDeltaTime / speed);

        public int LoadingOrder => 0;
        public PermanentGuid Guid => guid;


        private void Awake()
        {
            _smoothnessT = 1f / (1f + smoothness) * SMOOTHNESS_MODIFIER;
        }


        private void Update()
        {
            GetMovementThisFrame();
        }


        private void GetMovementThisFrame()
        {
            Vector3 input = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
            _newVelocity = speed * input;
        }


        private void FixedUpdate()
        {
            _velocity = Vector3.Lerp(_velocity, _newVelocity, _smoothnessT);

            if (_velocity == Vector3.zero)
            {
                return;
            }
            
            MoveAndRotate();
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
