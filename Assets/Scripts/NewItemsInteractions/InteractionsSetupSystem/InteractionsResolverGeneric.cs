using System;
using MoonPioneerClone.NewItemsInteractions.StackZoneLogic;
using MoonPioneerClone.NewItemsInteractions.Transfer.Target;
using UnityEngine;

namespace MoonPioneerClone.NewItemsInteractions.InteractionsSetupSystem
{
    public abstract class InteractionsResolver<TInteractions> : InteractionsResolver
    {
        [SerializeField] protected TInteractions interactions;
    }
}