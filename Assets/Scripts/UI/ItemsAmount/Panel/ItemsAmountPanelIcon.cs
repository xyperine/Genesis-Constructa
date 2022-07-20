using UnityEngine;
using UnityEngine.UI;

namespace ColonizationMobileGame.UI.ItemsAmount.Panel
{
    public class ItemsAmountPanelIcon : MonoBehaviour
    {
        [SerializeField] private Image image;


        public void SetData(Sprite icon)
        {
            SetVisibility(icon);

            if (!icon)
            {
                return;
            }

            image.sprite = icon;
            transform.SetSiblingIndex(0);
        }


        public void SetVisibility(bool visible)
        {
            gameObject.SetActive(visible && image.sprite);
        }
    }
}