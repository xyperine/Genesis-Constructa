using System;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    public class TestClass : MonoBehaviour, ISaveable
    {
        [SerializeField] private int number;
        [SerializeField] private string guid;

        public string Guid => guid;


        public void SetGuid(string newGuid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                guid = newGuid;
            }
        }


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