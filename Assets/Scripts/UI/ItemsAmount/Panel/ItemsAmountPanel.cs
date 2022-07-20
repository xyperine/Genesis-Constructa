using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.IconsSystem;
using ColonizationMobileGame.UI.ItemsAmount.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.UI.ItemsAmount.Panel
{
    public class ItemsAmountPanel : MonoBehaviour
    {
        [Header("Icon")]
        [SerializeField] private bool showIcon;
        [SerializeField, ShowIf(nameof(showIcon))] private IconsService iconsService;
        [SerializeField, ShowIf(nameof(showIcon))] private ItemsAmountPanelIcon iconObject;
        [Header("Misc")]
        [SerializeField] private ItemsAmountPanelData dataObject;
        [SerializeField] private ItemAmountPanelEntry entryPrefab;
        [SerializeField] private ItemsAmountPanelAnimator animator;
        
        private readonly Dictionary<ItemType, ItemAmountPanelEntry> _entries =
            new Dictionary<ItemType, ItemAmountPanelEntry>();
        
        private bool _visible;


        private void Awake()
        {
            CalculateVisibility();
            ApplyVisibility();
        }


        private void CalculateVisibility()
        {
            _visible = dataObject.ItemCounts != null &&
                       dataObject.ItemCounts.Any(ic => ic.Visible) &&
                       !dataObject.Locked;
        }


        private void ApplyVisibility()
        {
            foreach (ItemAmountPanelEntry entry in _entries.Values)
            {
                entry.SetVisibility(_visible);
            }
            
            iconObject.SetVisibility(_visible && showIcon);

            if (_visible)
            {
                animator.ScaleUp();
            }
            else
            {
                animator.ScaleDown();
            }
        }


        private void OnEnable()
        {
            dataObject.Changed += OnDataChanged;
            dataObject.Unlocked += OnUnlocked;
        }


        private void OnDisable()
        {
            dataObject.Changed -= OnDataChanged;
            dataObject.Unlocked -= OnUnlocked;
        }


        private void OnDataChanged()
        {
            CalculateVisibility();

            SetIcon();
            SetText();
            
            ApplyVisibility();
        }


        private void OnUnlocked()
        {
            CalculateVisibility();
            ApplyVisibility();
        }


        private void SetIcon()
        {
            if (!showIcon)
            {
                return;
            }

            Sprite icon = dataObject.Identifier == null ?
                null : iconsService.GetStructureIcon(dataObject.Identifier.StructureType);

            iconObject.SetData(icon);
        }


        private void SetText()
        {
            if (dataObject.ItemCounts == null)
            {
                return;
            }
            
            foreach (ItemAmountData itemCount in dataObject.ItemCounts)
            {
                if (!_entries.ContainsKey(itemCount.Type))
                {
                    _entries.TryAdd(itemCount.Type, InstantiateEntry());
                }

                _entries[itemCount.Type].SetData(itemCount);
            }
        }


        private ItemAmountPanelEntry InstantiateEntry()
        {
            return Instantiate(entryPrefab, transform);
        }
    }
}