using System.Collections.Generic;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public abstract class ArrowPointerTargetGroupValidator : MonoBehaviour
    {
        [SerializeField] private ArrowPointersDrawer drawer;

        protected readonly List<ArrowPointerTarget> targets = new List<ArrowPointerTarget>();


        public void RegisterTarget(ArrowPointerTarget target)
        {
            if (!targets.Contains(target))
            {
                targets.Add(target);
            }
            
            Debug.Log(targets.Count);
        }


        private void LateUpdate()
        {
            for (int i = targets.Count - 1; i >= 0; i--)
            {
                ArrowPointerTarget target = targets[i];
                if (TargetIsValid(target))
                {
                    drawer.Draw(target);
                }
                else
                {
                    drawer.StopDrawing(target);
                }
            }
        }


        protected abstract bool TargetIsValid(ArrowPointerTarget target);
    }
}