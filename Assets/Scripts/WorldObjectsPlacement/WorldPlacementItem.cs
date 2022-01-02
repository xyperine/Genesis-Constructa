using DG.Tweening;
using UnityEngine;

namespace MoonPioneerClone.WorldObjectsPlacement
{
    public class WorldPlacementItem : MonoBehaviour
    {
        public void Rotate()
        {
            transform.DOLocalRotateQuaternion(Quaternion.identity, 0.2f);
        }


        public void Move(Vector3 position)
        {
            transform.DOLocalMove(position, 0.2f);
        }
    }
}