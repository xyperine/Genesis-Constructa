using ColonizationMobileGame.Utility.Extensions;
using UnityEngine;

namespace ColonizationMobileGame.Hibernation.Area
{
    public class HibernationAreaCollider : MonoBehaviour
    {
        [SerializeField] private new BoxCollider collider;

        public bool ObjectInside { get; private set; }


        public void UpdateSize(float size)
        {
            collider.size = (Vector3.one * size).WithY(3f);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<HibernationObject>())
            {
                ObjectInside = true;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<HibernationObject>())
            {
                ObjectInside = false;
            }
        }
    }
}