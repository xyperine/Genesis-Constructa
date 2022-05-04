using UnityEngine;

namespace MoonPioneerClone.ItemsPlacementsInteractions.InteractionsSetup.Establisher
{
    public abstract class InteractionsEstablisher<TInteractions> : InteractionsEstablisher
    {
        [SerializeField] protected TInteractions interactions;
        
        
        public void Setup(TInteractions interactions)
        {
            this.interactions = interactions;
        }
    }
}