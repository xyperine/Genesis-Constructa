﻿using UnityEngine;

namespace ColonizationMobileGame.UI.ArrowPointers
{
    public abstract class ArrowPointerTarget
    {
        public abstract bool RequiresPointing { get; }
        public abstract Transform Transform { get; }
    }
}