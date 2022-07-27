using System;
using System.Linq;

namespace ColonizationMobileGame.ItemsRequirementsSystem
{
    public class ItemsRequirementsChain : IChain<ItemsRequirementsBlock>
    {
        private readonly ItemsRequirementsBlock[] _blocks;

        public bool NeedMore => Current is {NeedMore: true, Locked: false};
        public ItemType[] RequiredItems => Current?.RequiredItems;
        public ItemsRequirementsBlock Current { get; private set; }

        public event Action ChangingBlock;
        

        public ItemsRequirementsChain(ItemsRequirementsBlock[] blocks)
        {
            _blocks = blocks;
            
            SetupChain();
        }
        

        private void SetupChain()
        {
            if (!_blocks.Any())
            {
                return;
            }
            
            SetupInitialBlock();

            for (int i = 0; i < _blocks.Length; i++)
            {
                SetupBlock(i);
            }
        }


        private void SetupInitialBlock()
        {
            Current = _blocks[0];
            
            SubscribeToActiveBlockFulfilment();
        }


        private void SubscribeToActiveBlockFulfilment()
        {
            if (Current == null)
            {
                return;
            }

            Current.Fulfilled += GoToNextBlock;
        }


        private void GoToNextBlock()
        {
            UnsubscribeFromActiveBlockFulfilment();

            ChangingBlock?.Invoke();
            
            Current = Current.NextBlock;

            SubscribeToActiveBlockFulfilment();
        }


        private void UnsubscribeFromActiveBlockFulfilment()
        {
            if (Current == null)
            {
                return;
            }

            Current.Fulfilled -= GoToNextBlock;
        }


        private void SetupBlock(int index)
        {
            ItemsRequirementsBlock block = _blocks[index];

            if (index == _blocks.Length - 1)
            {
                return;
            }
            
            block.SetNextBlock(_blocks[index + 1]);
        }


        public void AddItem(ItemType type)
        {
            Current.AddItem(type);
        }
    }
}