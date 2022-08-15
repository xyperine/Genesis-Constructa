using UnityEngine;

namespace ColonizationMobileGame.ItemsExtraction.ConditionsLogic
{
    public abstract class ExtractorCondition : MonoBehaviour, ICondition
    {
        public abstract bool Met();
    }
}