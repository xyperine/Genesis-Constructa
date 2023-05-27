using System;
using UnityEngine;

namespace GenesisConstructa.GameEvents
{
    [CreateAssetMenu(fileName = "Shelter_Built_Event", menuName = "Game Events/Shelter Built Event", order = 0)]
    public class ShelterBuiltEventSO : ScriptableObject
    {
        public event Action Built;
        
        
        public void Raise()
        {
            Built?.Invoke();
        }
    }
}