using System;
using ColonizationMobileGame.Structures;
using UnityEngine;

namespace ColonizationMobileGame.ScoreSystem
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private ScoreRewardsSO scoreRewardsSO;
        
        private int _score;


        public void Add(ItemType itemType)
        {
            Add(scoreRewardsSO.GetScore(itemType));
        }
        
        
        public void Add(StructureType structureType)
        {
            Add(scoreRewardsSO.GetScore(structureType));
        }
        
        
        public void Add(int points)
        {
            if (points < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(points));
            }
            
            _score += points;
            
            Debug.Log(_score);
        }
    }
}