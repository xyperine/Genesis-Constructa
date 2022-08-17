using System.Linq;
using ColonizationMobileGame.Structures;
using UnityEngine;

namespace ColonizationMobileGame.IconsSystem
{
    [CreateAssetMenu(fileName = "Icons_Service", menuName = "Icons Service")]
    public class ObjectsIconsSO : ScriptableObject
    {
        [SerializeField] private StructureIcon[] structureIcons;


        public Sprite GetStructureIcon(StructureType type)
        {
            return structureIcons.SingleOrDefault(i => i.Type == type)?.Icon;
        }
    }
}