using System.Linq;
using GenesisConstructa.Structures;
using UnityEngine;

namespace GenesisConstructa.IconsSystem
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