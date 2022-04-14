using MoonPioneerClone.ItemsPlacementsInteractions;
using UnityEngine;

namespace MoonPioneerClone
{
    public abstract class Upgrader : MonoBehaviour
    {
        [SerializeField] private ItemsConsumer consumer;


        protected abstract void Upgrade();
    }
}