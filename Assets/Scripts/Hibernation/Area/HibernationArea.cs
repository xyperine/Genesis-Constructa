using System;
using UnityEngine;

namespace ColonizationMobileGame.Hibernation.Area
{
    public class HibernationArea : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private HibernationAreaDrawer areaDrawer;
        [SerializeField] private HibernationAreaCollider areaCollider;
        [SerializeField] private HibernationAreaIndicator areaIndicator;
        
        [Header("Misc")]
        [SerializeField, Range(1f, 4f)] private float size = 2f;
        [SerializeField, Min(0f)] private float goingIntoHibernationDuration = 3f;

        private float _secondsBeingInside;

        public event Action Hibernated;


        private void OnValidate()
        {
            areaCollider.UpdateSize(size);
            areaDrawer.UpdateRectangle(size);
        }


        private void Update()
        {
            if (!areaCollider.ObjectInside)
            {
                _secondsBeingInside = 0f;
                areaIndicator.UpdateFill(_secondsBeingInside, goingIntoHibernationDuration);
                return;
            }

            _secondsBeingInside += Time.deltaTime;
            areaIndicator.UpdateFill(_secondsBeingInside, goingIntoHibernationDuration);
        }


        private void LateUpdate()
        {
            if (_secondsBeingInside < goingIntoHibernationDuration)
            {
                return;
            }

            Hibernated?.Invoke();
            enabled = false;
        }
    }
}