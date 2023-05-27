using GenesisConstructa.UI.ItemsAmount.Data;
using TMPro;
using UnityEngine;

namespace GenesisConstructa.UI.ItemsAmount.Panel
{
    public class ItemAmountPanelEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private ItemAmountPanelEntryFormat _format;

        private bool _alwaysVisible;
        private bool _visible;


        public void Setup(ItemAmountPanelEntryFormat format, bool alwaysVisible)
        {
            _format = format;
            _alwaysVisible = alwaysVisible;
        }
        
        
        public void SetData(ItemAmountData data)
        {
            _visible = _alwaysVisible || data.GetVisibility(_format);
            SetVisibility(_visible);
            
            text.text = data.GetPresentation(_format);
            
            transform.SetSiblingIndex((int) data.Type + 1);
        }


        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible && _visible);
        }
    }
}