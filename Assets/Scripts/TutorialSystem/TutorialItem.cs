using ColonizationMobileGame.SaveLoadSystem;
using ColonizationMobileGame.UI.ArrowPointers;
using ColonizationMobileGame.UI.ArrowPointers.TargetGroupValidators;
using UnityEngine;

namespace ColonizationMobileGame.TutorialSystem
{
    public class TutorialItem : MonoBehaviour, IPermanentGuidIdentifiable, IArrowPointerTarget
    {
        [SerializeField] private TutorialStep step;

        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        public TutorialStep Step => step;
        public PermanentGuid Guid => guid;

        public bool RequiresPointing { get; private set; }


        public void Activate()
        {
            RequiresPointing = true;
            
            FindObjectOfType<TutorialArrowPointerTargetGroupValidator>().RegisterTarget(this);
        }
    }
}