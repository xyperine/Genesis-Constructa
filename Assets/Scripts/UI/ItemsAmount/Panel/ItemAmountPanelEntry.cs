using ColonizationMobileGame.UI.ItemsAmount.Data;
using TMPro;
using UnityEngine;

namespace ColonizationMobileGame.UI.ItemsAmount.Panel
{
    public class ItemAmountPanelEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private bool _visible;
        

        public void SetData(ItemAmountData data)
        {
            _visible = data.Visible;
            SetVisibility(_visible);
            
            text.text = data.Presentation;
            transform.SetSiblingIndex((int) data.Type + 1);
        }


        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible && _visible);
        }
    }
}