using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.UI.ItemsAmount.Panel
{
    public class ItemsAmountPanelProximityScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private ParticleSystem.MinMaxCurve range;
        
        private float _proximityFactor;
        
        private readonly Collider[] _colliders = new Collider[1];
        

        private void FixedUpdate()
        {
            GetProximity();
            SetScale();
        }


        private void GetProximity()
        {
            int numberOfColliders = Physics.OverlapSphereNonAlloc(rectTransform.position, range.constantMax, _colliders, playerLayer);

            if (numberOfColliders < 1)
            {
                return;
            }

            Vector3 closest = _colliders.Min(c => c.ClosestPoint(rectTransform.position));
            float distance = Vector3.Distance(rectTransform.position, closest);
            _proximityFactor = Mathf.InverseLerp(range.constantMax, range.constantMin, distance);
        }
        

        private void SetScale()
        {
            if (_proximityFactor <= 0)
            {
                return;
            }
            
            Vector3 newScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.3f, _proximityFactor);
            rectTransform.localScale = newScale;
        }
    }
}