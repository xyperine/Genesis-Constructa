using System;
using TMPro;
using UnityEngine;

namespace ColonizationMobileGame.UI
{
    public class ItemCountPanelEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private ItemCountPresentationMode _mode;
        private ItemCount _data;


        public void SetData(ItemCount data, ItemCountPresentationMode mode)
        {
            _mode = mode;
            _data = data;
            
            SetText();
            SetVisibility(!_data.Filled);

            transform.SetSiblingIndex((int) _data.Type);
        }


        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible);
        }
        

        private void SetText()
        {
            int type = (int) _data.Type;
            int current = _data.Current;
            int max = _data.Max;

            text.text = _mode switch
            {
                ItemCountPresentationMode.In => $"{type}: {current}",
                ItemCountPresentationMode.Remaining => $"{type}: {max - current}",
                ItemCountPresentationMode.Of => $"{type}: {current}/{max}",
                _ => throw new ArgumentOutOfRangeException(nameof(_data), _data, null),
            };
        }
    }
}