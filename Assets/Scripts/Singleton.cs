using UnityEngine;

namespace ColonizationMobileGame
{
    public abstract class Singleton<T> : MonoBehaviour
        where T : Singleton<T>
    {
        public static T Instance { get; private set; }


        protected virtual void Awake()
        {
            SetInstance();
        }


        private void SetInstance()
        {
            if (Instance)
            {
                Destroy(gameObject);
            }

            Instance = this as T;
        }
    }
}
