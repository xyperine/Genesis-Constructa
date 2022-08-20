using ColonizationMobileGame.Structures;
using UnityEngine;

namespace ColonizationMobileGame.ScoreSystem
{
    public class ScoreModifier : MonoBehaviour
    {
        [SerializeField] private Score score;
        [SerializeField] private ScoreRewardsSO scoreRewardsSO;


        public void Add(ItemType itemType)
        {
            Add(scoreRewardsSO.GetScore(itemType));
        }
        
        
        public void Add(StructureType structureType)
        {
            Add(scoreRewardsSO.GetScore(structureType));
        }
        
        
        public void Add(int value)
        {
            score.Add(value);
        }
    }
}