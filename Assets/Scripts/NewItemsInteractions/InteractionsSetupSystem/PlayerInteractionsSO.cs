using MoonPioneerClone.NewItemsInteractions.StackZoneLogic;
using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.InteractionsSetupSystem
{
    [CreateAssetMenu(fileName = "Player_Interactions", menuName = "Items Interactions/Player Interactions", order = 0)]
    public class PlayerInteractionsSO : ScriptableObject
    {
        [ShowInInspector, ReadOnly] private readonly InteractionsList _interactionsList = new InteractionsList();


        public void SetPlayer(TransferTarget player)
        {
            _interactionsList.SetHolder(player);
        }
        
        
        public void Add(TransferTarget target, StackZoneInteractionType type)
        {
            _interactionsList.TryAdd(target, type);
        }


        public bool CanTransferTo(TransferTarget target)
        {
            return _interactionsList.Exists(target, StackZoneInteractionType.Transfer);
        }
        
        
        public bool CanPickUpFrom(StackZone target)
        {
            return _interactionsList.Exists(target, StackZoneInteractionType.PickUp);
        }
    }
}