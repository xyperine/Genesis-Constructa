using System.Collections.Generic;
using ColonizationMobileGame.Structures;
using ColonizationMobileGame.Utility.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.ScoreSystem
{
    [CreateAssetMenu(fileName = "Score_Rewards_SO", menuName = "Score Rewards SO", order = 0)]
    public class ScoreRewardsSO : SerializedScriptableObject
    {
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        // ReSharper disable InconsistentNaming
        [SerializeField] private Dictionary<ItemType, int> scoreForEachItemInStorage =
            EnumHelpers.EnumToDictionary<ItemType, int>(0);

        // ReSharper disable once CollectionNeverUpdated.Local
        [SerializeField] private Dictionary<StructureType, int> scoreForUnlockingStructures =
            new Dictionary<StructureType, int>();
        // ReSharper restore InconsistentNaming
        // ReSharper restore FieldCanBeMadeReadOnly.Local


        public int GetScore(StructureType structureType)
        {
            scoreForUnlockingStructures.TryGetValue(structureType, out int score);
            return score;
        }


        public int GetScore(ItemType itemType)
        {
            scoreForEachItemInStorage.TryGetValue(itemType, out int score);
            return score;
        }
    }
}