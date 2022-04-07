using UnityEngine;

namespace MoonPioneerClone.ItemsExtraction.ConditionsLogic
{
    public abstract class ExtractorCondition : MonoBehaviour
    {
        public abstract bool Met();
    }
}