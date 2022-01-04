using System;
using MoonPioneerClone.CollectableItemsInteractions;
using UnityEngine;

namespace MoonPioneerClone.Utility.Exceptions
{
    public sealed class NoCollectorInteractableAttachedException : NullReferenceException
    {
        public NoCollectorInteractableAttachedException(GameObject otherGameObject) : base(GetMessage(otherGameObject)) {  }


        private static string GetMessage(GameObject otherGameObject)
        {
            return $"No { nameof(ICollectorInteractable) } component attached to the { otherGameObject }!" +
                   $" Consider attaching { nameof(ICollectorInteractable) } component or tweak Layer Collisions Matrix";
        }
    }
}
