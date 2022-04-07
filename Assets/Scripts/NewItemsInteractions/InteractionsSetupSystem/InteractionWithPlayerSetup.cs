using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.InteractionsSetupSystem
{
    public class InteractionWithPlayerSetup : MonoBehaviour
    {
        [SerializeField] protected bool interactWithPlayer;
        [SerializeField, ShowIf(nameof(interactWithPlayer))] private StackZoneInteractionType interactionType;
        [SerializeField, ShowIf(nameof(interactWithPlayer))] private PlayerInteractionsSO playerInteractionsSO;

        
#if UNITY_EDITOR
        private void OnValidate()
        {
            SetUpInteractionWithPlayer();            
        }
#else
        private void Awake()
        {
            SetUpInteractionWithPlayer();            
        }
#endif
        
        
        private void SetUpInteractionWithPlayer()
        {
            if (!playerInteractionsSO)
            {
                return;
            }
            
            if (interactWithPlayer)
            {
                playerInteractionsSO.Add(GetComponent<TransferTarget>(), interactionType);
            }
        }
    }
}