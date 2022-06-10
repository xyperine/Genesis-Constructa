using UnityEngine;

namespace ColonizationMobileGame.Player
{
    public class RobotAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerMovement playerMovement;


        private void Update()
        {
            animator.SetFloat("Speed", playerMovement.RelativeVelocity);
        }
    }
}