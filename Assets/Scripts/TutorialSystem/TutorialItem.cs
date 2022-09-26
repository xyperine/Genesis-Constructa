using System;
using ColonizationMobileGame.UI.ArrowPointers.Target;
using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem
{
    public class TutorialItem : MonoBehaviour, IArrowPointerTargetProvider
    {
        [SerializeField] private TutorialStep step;
        
        public TutorialStep Step => step;
        
        public event Action<Transform> TargetReady;


        public void Activate()
        {
            TargetReady?.Invoke(transform);
        }
    }
}