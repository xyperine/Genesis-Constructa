using UnityEngine;

namespace ColonizationMobileGame.Utility
{
    public class LocalizedSingletonMonoBehaviour<T> : ISingletonBehaviour<T> 
        where T : MonoBehaviour
    {
        private static T Instance { get; set; }
        
        
        public void SetInstance(T instance)
        {
            if (Instance)
            {
                Object.Destroy(instance);
            }

            Instance = instance;
        }
    }
}