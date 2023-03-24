using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItemsSet : MonoBehaviour
    {
        [InfoBox("Please, order slots in hierarchy by their key item duration short -> long")]
        [SerializeField] private KeyItemSlot[] slots;

        public bool Filled => slots.All(s => s.Filled);


        private void Awake()
        {
            slots = GetComponentsInChildren<KeyItemSlot>();
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
            float minLifetime = float.MaxValue;
            foreach (KeyItemSlot slot in slots)
            {
                minLifetime = Mathf.Min(minLifetime, slot.Lifetime);
                slot.Tick();
            }
            
            if (Filled)
            {
                return;
            }

            foreach (KeyItemSlot slot in slots)
            {
                slot.Adjust(minLifetime);
            }
        }
    }
}