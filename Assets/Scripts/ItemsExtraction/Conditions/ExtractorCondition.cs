using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction.Conditions
{
    public abstract class ExtractorCondition : MonoBehaviour
    {
        public abstract bool Met();
    }
}