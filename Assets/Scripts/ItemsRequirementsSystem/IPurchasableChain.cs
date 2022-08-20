﻿namespace ColonizationMobileGame.ItemsRequirementsSystem
{
    public interface IPurchasableChain<out T> : IChain<T>
    {
        ItemsRequirementsChain RequirementsChain { get; }
    }
}