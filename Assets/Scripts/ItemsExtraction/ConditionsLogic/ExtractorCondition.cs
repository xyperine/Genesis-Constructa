using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.ConditionsLogic
{
    public abstract class ExtractorCondition : MonoBehaviour
    {
        public abstract bool Met();
    }
}