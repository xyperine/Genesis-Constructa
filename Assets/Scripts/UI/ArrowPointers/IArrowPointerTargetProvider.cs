using System;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public interface IArrowPointerTargetProvider
    {
        public event Action<Transform, ArrowPointerTargetCondition> TargetReady;
    }
}