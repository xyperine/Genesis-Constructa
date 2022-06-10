using System.Linq;

namespace ColonizationMobileGame.ItemsRequirementsSystem
{
    public class ItemsRequirementsChain
    {
        private readonly ItemsRequirementsBlock[] _blocks;

        private ItemsRequirementsBlock _activeBlock;

        public ItemType[] RequiredItems => _activeBlock?.RequiredItems;
        public bool NeedMore => _activeBlock is {NeedMore: true};


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
            _activeBlock = _blocks[0];
            
            SubscribeToActiveBlockSatisfaction();
        }


        private void SubscribeToActiveBlockSatisfaction()
        {
            if (_activeBlock == null)
            {
                return;
            }

            _activeBlock.Satisfied += GoToNextBlock;
        }


        private void UnsubscribeFromActiveBlockSatisfaction()
        {
            if (_activeBlock == null)
            {
                return;
            }

            _activeBlock.Satisfied -= GoToNextBlock;
        }


        private void GoToNextBlock()
        {
            UnsubscribeFromActiveBlockSatisfaction();

            _activeBlock = _activeBlock.NextBlock;

            SubscribeToActiveBlockSatisfaction();
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
            _activeBlock.AddItem(type);
        }
    }
}