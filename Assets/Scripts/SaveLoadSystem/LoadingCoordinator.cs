using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ColonizationMobileGame.SaveLoadSystem
{
    public class LoadingCoordinator : MonoBehaviour
    {
        [SerializeField, ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        private List<SaveableType> order = Enum.GetValues(typeof(SaveableType)).Cast<SaveableType>().ToList();
        
        
        public List<ISceneSaveable> OrderData(IEnumerable<ISceneSaveable> initialData)
        {
            return initialData.OrderBy(s => order.IndexOf(s.SaveableType)).ToList();
        }
    }
}