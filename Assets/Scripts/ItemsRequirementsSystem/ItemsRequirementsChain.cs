using System.Linq;

namespace ColonizationMobileGame.ItemsRequirementsSystem
{
    public class ItemsRequirementsChain
    {
        private readonly ItemsRequirementsBlock[] _blocks;

        public bool NeedMore => CurrentBlock is {NeedMore: true, Locked: false};
        public ItemType[] RequiredItems => CurrentBlock?.RequiredItems;
        public ItemsRequirementsBlock CurrentBlock { get; private set; }


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
            CurrentBlock = _blocks[0];
            
            SubscribeToActiveBlockSatisfaction();
        }


        private void SubscribeToActiveBlockSatisfaction()
        {
            if (CurrentBlock == null)
            {
                return;
            }

            CurrentBlock.Fulfilled += GoToNextBlock;
        }


        private void GoToNextBlock()
        {
            UnsubscribeFromActiveBlockSatisfaction();

            CurrentBlock = CurrentBlock.NextBlock;

            SubscribeToActiveBlockSatisfaction();
        }


        private void UnsubscribeFromActiveBlockSatisfaction()
        {
            if (CurrentBlock == null)
            {
                return;
            }

            CurrentBlock.Fulfilled -= GoToNextBlock;
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
            CurrentBlock.AddItem(type);
        }
    }
}