﻿using GenesisConstructa.SaveLoadSystem;
using UnityEngine;

namespace GenesisConstructa.TutorialSystem
{
    public class TutorialItem : MonoBehaviour, IPermanentGuidIdentifiable
    {
        [SerializeField] private TutorialStep step;

        [SerializeField, HideInInspector] private PermanentGuid guid;
        
        public TutorialStep Step => step;
        public PermanentGuid Guid => guid;


        public void Activate()
        {
            
        }
    }
}