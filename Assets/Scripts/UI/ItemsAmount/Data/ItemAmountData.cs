using System;

namespace ColonizationMobileGame.UI.ItemsAmount.Data
{
    public class ItemAmountData
    {
        private readonly int _current;
        private readonly int _max;

        public ItemType Type { get; }


        public string GetPresentation(ItemAmountPanelEntryFormat format)
        {
            return format switch
            {
                ItemAmountPanelEntryFormat.In => $"<sprite={(int)Type}> {_current}",
                ItemAmountPanelEntryFormat.Left => $"<sprite={(int)Type}> {_max - _current}",
                ItemAmountPanelEntryFormat.Of => $"<sprite={(int)Type}> {_current}/{_max}",
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, null),
            };
        }


        public bool GetVisibility(ItemAmountPanelEntryFormat format)
        {
            return format switch
            {
                ItemAmountPanelEntryFormat.In => _current > 0,
                ItemAmountPanelEntryFormat.Left => _current < _max,
                ItemAmountPanelEntryFormat.Of => _current < _max,
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, null),
            };
        }
        

        public ItemAmountData(ItemType type, int current, int max = -1)
        {
            if (max != -1)
            {
                if (max <= 0)
                {
                    throw new Exception($"{nameof(max)} has to be more than 0 in order to be valid!");
                }

                if (max < current)
                {
                    throw new Exception($"{nameof(max)} is less than {nameof(current)}!");
                }
            }

            _current = current;
            _max = max;
            Type = type;
        }
    }
}