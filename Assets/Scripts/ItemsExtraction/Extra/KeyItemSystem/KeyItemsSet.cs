using System.Linq;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItemsSet : MonoBehaviour
    {
        [SerializeField] private KeyItemSlot[] slots;
        
        public bool Satisfied => slots.All(s => s.Satisfied);


        private void Update()
        {
            if (!Satisfied)
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