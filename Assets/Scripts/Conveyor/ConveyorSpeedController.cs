using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.Conveyor
{
    public class ConveyorSpeedController : MonoBehaviour
    {
        [SerializeField] private ConveyorPowerController powerController;
        [SerializeField] private ConveyorBeltRenderer beltRenderer;
        
        [InfoBox("Adjusting this in Edit mode will change the original material speed", InfoMessageType.Warning)]
        [SerializeField, Range(-1f, 1f)] private float speed;


        public float Speed { get; private set; }


        private void OnValidate()
        {
            beltRenderer.UpdateMaterial(speed);
        }


        private void OnEnable()
        {
            powerController.PoweredStatusChanged += ChangeSpeed;
        }


        private void ChangeSpeed()
        {
            Speed = powerController.Powered ? 
                speed : 0f;
            
            beltRenderer.UpdateMaterial(Speed);
        }
        
        
        private void OnDisable()
        {
            powerController.PoweredStatusChanged -= ChangeSpeed;
        }
    }
}