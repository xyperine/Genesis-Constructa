using System;
using GenesisConstructa.SaveLoadSystem;
using UnityEngine;

namespace GenesisConstructa.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItem : MonoBehaviour, ISaveable
    {
        [SerializeField, Range(5f, 180f)] private float lifetime;
        
        private float _elapsedTime;

        public bool Exhausted => _elapsedTime >= lifetime;
        public float Lifetime => lifetime;


        public void Tick()
        {
            _elapsedTime += Time.deltaTime;
        }


        public void Adjust(float n)
        {
            _elapsedTime = Mathf.Round(_elapsedTime / n) * n;
        }


        public void Clear()
        {
            _elapsedTime = 0f;
        }


        public object Save()
        {
            return new SaveData
            {
                ElapsedTime = _elapsedTime,
            };
        }


        public void Load(object data)
        {
            SaveData saveData = (SaveData) data;

            _elapsedTime = saveData.ElapsedTime;
        }


        [Serializable]
        private struct SaveData
        {
            public float ElapsedTime { get; set; }
        }
    }
}