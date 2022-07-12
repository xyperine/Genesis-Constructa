using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ColonizationMobileGame.UI
{
    public class ItemsCountPanel : MonoBehaviour
    {
        [SerializeField] private ItemsCountPanelData data;
        [SerializeField] private Transform linesParent;
        [SerializeField] private GameObject linePrefab;

        [SerializeField] private ItemCountPresentationMode mode;

        private readonly Dictionary<ItemType, ItemCountPanelEntry> _entries =
            new Dictionary<ItemType, ItemCountPanelEntry>();


        private void Awake()
        {
            data.Changed += SetText;
            data.ValidityChanged += SetVisibility;
            
            SetVisibility();
        }
        
        
        private void OnDisable()
        {
            data.Changed -= SetText;
            data.ValidityChanged -= SetVisibility;
        }


        private void SetText()
        {
            if (!data.Valid)
            {
                return;
            }

            foreach (ItemCount itemCount in data.Items)
            {
                if (!_entries.ContainsKey(itemCount.Type))
                {
                    _entries.TryAdd(itemCount.Type, InstantiateLine());
                }
                
                _entries[itemCount.Type].SetData(itemCount, mode);
            }
        }


        private void SetVisibility()
        {
            GetComponent<Image>().enabled = data.Valid;
            foreach (ItemCountPanelEntry entry in _entries.Values)
            {
                entry.SetVisibility(data.Valid);
            }

            if (data.Valid)
            {
                SetText();
            }
        }


        private ItemCountPanelEntry InstantiateLine()
        {
            GameObject lineObject = Instantiate(linePrefab, linesParent);
            return lineObject.GetComponent<ItemCountPanelEntry>();
        }
    }
}