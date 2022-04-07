using UnityEngine;

namespace MoonPioneerClone.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private float speed;


        private void Update()
        {
            if (joystick.Direction == Vector2.zero)
            {
                return;
            }

            MoveAndRotate();
        }


        private void MoveAndRotate()
        {
            Vector3 input = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

            Vector3 newPosition = transform.position + speed * Time.deltaTime * input;
            Quaternion newRotation = Quaternion.LookRotation(-input);

            transform.SetPositionAndRotation(newPosition, newRotation);
        }
    }
}
