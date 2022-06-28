using ColonizationMobileGame.ItemsPlacementsInteractions;
using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.Extra.KeyItemSystem
{
    public class KeyItem : MonoBehaviour
    {
        [SerializeField] private StackZoneItem item;
        [SerializeField, Range(5f, 60f)] private float lifetime;

        private float _elapsedTime;
        public bool Exhausted { get; private set; }


        public void Tick()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= lifetime)
            {
                Exhausted = true;
            }
        }
    }
}