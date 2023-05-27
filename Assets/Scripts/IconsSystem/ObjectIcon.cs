using System;
using UnityEngine;

namespace GenesisConstructa.IconsSystem
{
    [Serializable]
    public abstract class ObjectIcon<T>
        where T : Enum
    {
        [SerializeField] private T type;
        [SerializeField] private Sprite icon;

        public T Type => type;
        public Sprite Icon => icon;
    }
}