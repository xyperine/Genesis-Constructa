using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ItemsPlacementsInteractions.Target;
using ColonizationMobileGame.Utility.Observing;
using UnityEngine;

namespace ColonizationMobileGame.ItemsPlacementsInteractions.InteractionsSetup
{
    [Serializable]
    public class InteractionsList
    {
        [SerializeField] private ObservingCollection<Interaction> interactions = new ObservingCollection<Interaction>();
        [SerializeField, HideInInspector] private InteractionTarget holder;

        public IEnumerable<InteractionType> InteractionTypes => interactions.Select(i => i.Type).Distinct();


        public InteractionsList()
        {
            interactions.Changed += CheckForBadInteractions;
        }


        private void CheckForBadInteractions()
        {
            if (HasInteractionWith(holder))
            {
                Debug.LogError($"{holder}: I can't interact with myself!");
            }

            Interaction repeatedInteraction =
                interactions.FirstOrDefault(i1 =>
                    interactions.Count(i2 => i1.Target == i2.Target && i1.Target && i1.Target != holder) > 1);
            
            if (repeatedInteraction != null)
            {
                Debug.LogError($"{holder}: Interaction with {repeatedInteraction.Target} already exists!");
            }
        }
        
        
        public void SetHolder(InteractionTarget holder)
        {
            this.holder = holder;
        }
        
        
        public void TryAdd(InteractionTarget target, InteractionType type)
        {
            TryAdd(new Interaction(target, type));
        }
        
        
        public void TryAdd(Interaction interaction)
        {
            if (!interaction.Target)
            {
                Debug.LogError($"{holder}: Trying to add interaction with null!");
                return;
            }
            
            if (interaction.Target == holder)
            {
                Debug.LogError($"{holder}: Trying to add interaction with myself!");
                return;
            }
            
            if (HasInteractionWith(interaction.Target))
            {
                Debug.LogError($"{holder}: Trying to add another interaction with {interaction.Target}!");
                return;
            }
            
            interactions.Add(interaction);
        }


        private bool HasInteractionWith(InteractionTarget target)
        {
            return interactions.Any(i => i.Target == target);
        }


        public bool Exists(Interaction interaction)
        {
            return Exists(interaction.Target, interaction.Type);
        }
        
        
        public bool Exists(InteractionTarget target, InteractionType type)
        {
            return interactions.Any(i => i.Target == target && i.Type == type);
        }


        public void Clear()
        {
            SetHolder(null);
            interactions.Clear();
        }
    }
}