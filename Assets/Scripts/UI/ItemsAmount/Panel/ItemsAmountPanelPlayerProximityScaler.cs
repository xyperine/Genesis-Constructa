using System.Linq;
using UnityEngine;

namespace GenesisConstructa.UI.ItemsAmount.Panel
{
    public class ItemsAmountPanelPlayerProximityScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private ParticleSystem.MinMaxCurve range;
        [SerializeField] private Transform targetTransform;

        private float _playerProximityFactor;

        private readonly Collider[] _colliders = new Collider[4];
        

        private void FixedUpdate()
        {
            GetProximity();
            SetScale();
        }


        private void GetProximity()
        {
            int numberOfColliders = Physics.OverlapSphereNonAlloc(targetTransform.position, range.constantMax, _colliders, playerLayer);

            if (numberOfColliders < 1)
            {
                return;
            }

            Collider collider = _colliders.SingleOrDefault(c => c != null && c.CompareTag("PlayerCollider"));

            if (!collider)
            {
                return;
            }

            Vector3 colliderCenter = collider.transform.position;
            float distance = Vector3.Distance(targetTransform.position, colliderCenter);
            _playerProximityFactor = Mathf.InverseLerp(range.constantMax, range.constantMin, distance);
        }
        

        private void SetScale()
        {
            if (_playerProximityFactor <= 0)
            {
                return;
            }

            float t = _playerProximityFactor * _playerProximityFactor;
            Vector3 newScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.3f, t);
            rectTransform.localScale = newScale;
        }
    }
}