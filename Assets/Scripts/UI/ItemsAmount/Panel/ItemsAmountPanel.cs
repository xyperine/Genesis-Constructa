﻿using System.Collections.Generic;
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
        [SerializeField, ShowIf(nameof(showIcon))] private ObjectsIconsSO objectsIconsSO;
        [SerializeField, ShowIf(nameof(showIcon))] private ItemsAmountPanelIcon iconObject;
        [Header("Text")]
        [SerializeField] private ItemAmountPanelEntryFormat format;
        [Header("Misc")] 
        [SerializeField] private bool alwaysVisible;
        [SerializeField] private ItemsAmountPanelData dataObject;
        [SerializeField] private ItemAmountPanelEntry entryPrefab;
        [Header("Visuals")] 
        [SerializeField] private bool animate = true;
        [SerializeField, ShowIf(nameof(animate))] private ItemsAmountPanelAnimator animator;
        [SerializeField] private ItemsAmountPanelShapesContentFitter shapesContentFitter;
        
        private readonly Dictionary<ItemType, ItemAmountPanelEntry> _entries =
            new Dictionary<ItemType, ItemAmountPanelEntry>();
        
        private bool _visible;


        private void Awake()
        {
            Initialize();
        }


        private void Initialize()
        {
            CalculateVisibility();
            ApplyVisibility();
            FitContent();
        }


        private void CalculateVisibility()
        {
            _visible = alwaysVisible ||
                       (dataObject.ItemCounts != null &&
                       dataObject.ItemCounts.Any(ic => alwaysVisible || ic.GetVisibility(format)) &&
                       !dataObject.Locked);
        }


        private void ApplyVisibility()
        {
            foreach (ItemAmountPanelEntry entry in _entries.Values)
            {
                entry.SetVisibility(_visible);
            }

            if (iconObject)
            {
                iconObject.SetVisibility(_visible && showIcon);
            }

            shapesContentFitter.SetVisibility(_visible);

            if (animate)
            {
                Animate();
            }
        }


        private void Animate()
        {
            if (!animator)
            {
                return;
            }
            
            if (_visible)
            {
                animator.ScaleUp();
            }
            else
            {
                animator.ScaleDown();
            }
        }


        private void FitContent()
        {
            shapesContentFitter.ResizeBackground();
        }


        private void OnEnable()
        {
            dataObject.Changed += OnDataChanged;
            dataObject.Unlocked += OnUnlocked;
            
            OnDataChanged();
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
            
            FitContent();
        }


        private void OnUnlocked()
        {
            Initialize();
        }


        private void SetIcon()
        {
            if (!showIcon)
            {
                return;
            }

            Sprite icon = dataObject.Identifier == null ?
                null : objectsIconsSO.GetStructureIcon(dataObject.Identifier.StructureType);

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
            ItemAmountPanelEntry entry = Instantiate(entryPrefab, transform);
            entry.Setup(format, alwaysVisible);

            return entry;
        }
    }
}