using UnityEngine;

namespace MoonPioneerClone.ItemsInteractions.StackZoneLogic.Upgrading
{
    [CreateAssetMenu(fileName = "Stack_Zone_Upgrades", menuName = "Upgrades/Stack Zone", order = 0)]
    public sealed class StackZoneUpgradesSO : ScriptableObject
    {
        [SerializeField] private StackZoneUpgradeData[] upgrades;

        private int _index;

        public int Capacity => upgrades[_index].Capacity;


        public void Upgrade()
        {
            if (_index >= upgrades.Length)
            {
                return;
            }
            
            _index++;
        }
    }
}