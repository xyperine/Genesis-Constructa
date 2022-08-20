using System.Collections.Generic;
using ColonizationMobileGame.Structures;
using ColonizationMobileGame.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.ScoreSystem
{
    [CreateAssetMenu(fileName = "Score_Rewards_SO", menuName = "Score Rewards SO", order = 0)]
    public class ScoreRewardsSO : SerializedScriptableObject
    {
        [SerializeField]
        private Dictionary<StructureType, int> structuresUnlockingPoints = Helpers.EnumToDictionary<StructureType, int>(0);
        [SerializeField]
        private Dictionary<ItemType, int> itemsInStoragePoints = Helpers.EnumToDictionary<ItemType, int>(0);


        public int GetScore(StructureType structureType)
        {
            structuresUnlockingPoints.TryGetValue(structureType, out int score);
            return score;
        }


        public int GetScore(ItemType itemType)
        {
            itemsInStoragePoints.TryGetValue(itemType, out int score);
            return score;
        }
    }
}