using MoonPioneerClone.ItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone
{
    public abstract class Upgrader : MonoBehaviour
    {
        [SerializeField] private ItemsConsumer consumer;
        
        
        private void Awake()
        {
            consumer.BlockSatisfied += Upgrade;
        }


        protected abstract void Upgrade();
    }
}