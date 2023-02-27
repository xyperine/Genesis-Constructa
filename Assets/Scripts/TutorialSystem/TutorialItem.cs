using System;
using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.UI.ArrowPointers.Target;
using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem
{
    public class TutorialItem : MonoBehaviour, IArrowPointerTargetProvider, IPermanentGuidIdentifiable
    {
        [SerializeField] private TutorialStep step;

        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        public TutorialStep Step => step;
        public PermanentGuid Guid => guid;

        public event Action<Transform> TargetReady;


        public void Activate()
        {
            TargetReady?.Invoke(transform);
        }
    }
}