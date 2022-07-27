using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItem : MonoBehaviour
    {
        [SerializeField, Range(5f, 180f)] private float lifetime;

        private float _elapsedTime;
        public bool Exhausted => _elapsedTime >= lifetime;
        public bool WillBeExhausted => _elapsedTime + Time.deltaTime >= lifetime;


        public void Tick()
        {
            _elapsedTime += Time.deltaTime;
        }


        public void Clear()
        {
            _elapsedTime = 0f;
        }
    }
}