using ColonizationMobileGame.ItemsPlacementsInteractions;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItem : MonoBehaviour
    {
        [SerializeField] private StackZoneItem item;
        [SerializeField, Range(5f, 60f)] private float lifetime;

        private float _elapsedTime;
        public bool Exhausted => _elapsedTime > lifetime;


        public void Tick()
        {
            if (Exhausted)
            {
                _elapsedTime = 0f;
            }

            _elapsedTime += Time.deltaTime;
        }
    }
}