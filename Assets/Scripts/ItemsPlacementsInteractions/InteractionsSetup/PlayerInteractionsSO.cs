using GenesisConstructa.ItemsPlacementsInteractions.StackZoneLogic;
using GenesisConstructa.ItemsPlacementsInteractions.Target;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenesisConstructa.ItemsPlacementsInteractions.InteractionsSetup
{
    [CreateAssetMenu(fileName = "Player_Interactions", menuName = "Items Interactions/Player Interactions", order = 0)]
    public class PlayerInteractionsSO : ScriptableObject
    {
        [ShowInInspector, ReadOnly] private readonly InteractionsList _interactionsList = new InteractionsList();


        public void SetPlayer(InteractionTarget player)
        {
            _interactionsList.SetHolder(player);
        }
        
        
        public void Register(InteractionTarget target, InteractionType type)
        {
            _interactionsList.TryAdd(target, type);
        }


        public bool CanTransferTo(InteractionTarget target)
        {
            return _interactionsList.Exists(target, InteractionType.Transfer);
        }
        
        
        public bool CanPickUpFrom(StackZone target)
        {
            return _interactionsList.Exists(target, InteractionType.PickUp);
        }
    }
}