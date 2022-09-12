using System;
using System.Collections;
using ColonizationMobileGame.Utility;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public class ArrowPointerTargetCondition
    {
        public bool Met { get; private set; }

        public event Action Satisfied; 


        public ArrowPointerTargetCondition()
        {
            //Object.FindObjectOfType<TargetsManager>().StartCoroutine(Coroutine());

            IEnumerator Coroutine()
            {
                yield return Helpers.GetWaitForSeconds(5f);
                
                InvokeSatisfied();
            }
        }


        private void InvokeSatisfied()
        {
            Met = true;
            Satisfied?.Invoke();
        }
    }
}