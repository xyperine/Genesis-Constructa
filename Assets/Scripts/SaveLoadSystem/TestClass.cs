using System;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    public class TestClass : MonoBehaviour, ISaveable
    {
        [SerializeField] private int number;
        [SerializeField] private PermanentGuid guid;

        public PermanentGuid Guid => guid;


        public object Save()
        {
            return new SaveData
            {
                Number = number,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            number = saveData.Number;
        }
        
        
        [Serializable]
        private struct SaveData
        {
            public int Number { get; set; }
        }
    }
}