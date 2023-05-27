using System;
using UnityEngine;

namespace GenesisConstructa.GameEvents
{
    [CreateAssetMenu(fileName = "Hibernation_Area_Event", menuName = "Game Events/Hibernation Area Event", order = 0)]
    public class HibernationAreaEventSO : ScriptableObject
    {
        public event Action<bool> Changed; 


        public void Raise(bool objectInside)
        {
            Changed?.Invoke(objectInside);
        }
    }
}