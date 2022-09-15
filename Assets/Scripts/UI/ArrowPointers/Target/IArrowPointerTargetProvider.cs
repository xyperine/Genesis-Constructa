using System;
using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers.Target
{
    public interface IArrowPointerTargetProvider
    {
        public event Action<Transform> TargetReady;
    }
}