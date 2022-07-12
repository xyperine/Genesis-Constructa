using System;
using System.Linq;
using ColonizationMobileGame.UnlockingSystem;
using UnityEngine;

namespace ColonizationMobileGame.UI
{
    public class ItemsCountPanelData : MonoBehaviour
    {
        private bool _prevValid;
        private bool _valid;

        public ItemCount[] Items { get; private set; }

        public bool Valid
        {
            get => _valid;
            private set
            {
                _prevValid = _valid;
                _valid = value;
                
                if (value != _prevValid)
                {
                    InvokeValidityChanged();
                }
            }
        }

        public event Action ValidityChanged;
        public event Action Changed;


        public void SetData(ItemCount data)
        {
            SetData(new[] {data}, null);
        }


        public void SetData(ItemCount[] data, IUnlockable unlockable)
        {
            Items = data;
            
            SetValidity();
            
            if (unlockable == null)
            {
                Changed?.Invoke();
                return;
            }
            
            Valid &= !unlockable.Locked;

            if (unlockable.Locked)
            {
                unlockable.Unlocked += SetValidity;
            }

            Changed?.Invoke();
        }


        private void SetValidity()
        {
            Valid = Items != null && Items.Any();
        }
        

        private void InvokeValidityChanged()
        {
            ValidityChanged?.Invoke();
        }
    }
}