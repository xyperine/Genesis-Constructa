using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItemsSet : MonoBehaviour
    {
        [SerializeField] private KeyItemSlot[] slots;
        
        public bool Filled => slots.All(s => s.Filled);


        private void Awake()
        {
            slots = GetComponentsInChildren<KeyItemSlot>();
            
            // if (!slots.Any())
            // {
            //     throw new NotImplementedException($"You have to put key item slots in {slots} array!");
            // }
        }


        private void Update()
        {
            if (!Filled)
            {
                return;
            }
            
            UpdateEverySlot();
        }


        private void UpdateEverySlot()
        {
            foreach (KeyItemSlot slot in slots)
            {
                slot.Tick();
            }
        }
    }
}