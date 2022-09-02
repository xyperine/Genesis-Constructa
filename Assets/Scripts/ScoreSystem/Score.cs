using System;
using ColonizationMobileGame.SaveLoadSystem;
using UnityEngine;

namespace ColonizationMobileGame.ScoreSystem
{
    public class Score : MonoBehaviour, ISceneSaveable
    {
        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        public int Value { get; private set; }
        
        public SaveableType SaveableType => SaveableType.Other;
        public PermanentGuid Guid => guid;


        public void Add(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            
            Value += value;
            
            Debug.Log(Value);
        }


        public object Save()
        {
            return new SaveData
            {
                Value = Value,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            Value = saveData.Value;
        }


        [Serializable]
        private struct SaveData
        {
            public int Value { get; set; }
        }
    }
}