using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.ItemsPlacementsInteractions.Transfer.Target;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup
{
    [CreateAssetMenu(fileName = "Player_Interactions", menuName = "Items Interactions/Player Interactions", order = 0)]
    public class PlayerInteractionsSO : ScriptableObject
    {
        [ShowInInspector, ReadOnly] private readonly InteractionsList _interactionsList = new InteractionsList();


        public void SetPlayer(TransferTarget player)
        {
            _interactionsList.SetHolder(player);
        }
        
        
        public void Register(TransferTarget target, InteractionType type)
        {
            _interactionsList.TryAdd(target, type);
        }


        public bool CanTransferTo(TransferTarget target)
        {
            return _interactionsList.Exists(target, InteractionType.Transfer);
        }
        
        
        public bool CanPickUpFrom(StackZone target)
        {
            return _interactionsList.Exists(target, InteractionType.PickUp);
        }
    }
}