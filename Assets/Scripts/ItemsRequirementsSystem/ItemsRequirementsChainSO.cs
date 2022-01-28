using System;
using System.Linq;
using UnityEngine;

namespace MoonPioneerClone.ItemsRequirementsSystem
{
    [CreateAssetMenu(fileName = "ItemsRequirementsChain", menuName = "Items Requirements Chain")]
    public sealed class ItemsRequirementsChainSO : ScriptableObject
    {
        [SerializeField] private ItemsRequirementsBlock[] blocks;

        private ItemsRequirementsBlock _activeBlock;

        public ItemType[] RequiredItems => _activeBlock?.RequiredItems;
        public bool NeedMore => _activeBlock.NeedMore;

        public event Action BlockSatisfied;


        private void OnValidate()
        {
            SetupChain();
        }


        private void SetupChain()
        {
            if (!blocks.Any())
            {
                return;
            }
            
            SetupInitialBlock();

            for (int i = 0; i < blocks.Length; i++)
            {
                SetupBlock(i);
            }
        }


        private void SetupInitialBlock()
        {
            _activeBlock = blocks[0];
            
            SubscribeToActiveBlockSatisfaction();
        }


        private void SubscribeToActiveBlockSatisfaction()
        {
            if (_activeBlock == null)
            {
                return;
            }

            _activeBlock.Satisfied += GoToNextBlock;
            _activeBlock.Satisfied += InvokeBlockSatisfied;
        }


        private void UnsubscribeFromActiveBlockSatisfaction()
        {
            if (_activeBlock == null)
            {
                return;
            }

            _activeBlock.Satisfied -= GoToNextBlock;
            _activeBlock.Satisfied -= InvokeBlockSatisfied;
        }


        private void GoToNextBlock()
        {
            UnsubscribeFromActiveBlockSatisfaction();

            _activeBlock = _activeBlock.NextBlock;

            SubscribeToActiveBlockSatisfaction();
        }


        private void InvokeBlockSatisfied()
        {
            BlockSatisfied?.Invoke();
        }


        private void SetupBlock(int index)
        {
            ItemsRequirementsBlock block = blocks[index];
#if UNITY_EDITOR
            block.UpdateCounter();
#endif
            
            if (index == blocks.Length - 1)
            {
                return;
            }
            
            block.SetNextBlock(blocks[index + 1]);
        }


        public void AddItem(ItemType type)
        {
            _activeBlock.AddItem(type);
        }
    }
}