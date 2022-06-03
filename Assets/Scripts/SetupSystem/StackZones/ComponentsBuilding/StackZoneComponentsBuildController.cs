using System;
using System.Collections.Generic;
using System.Linq;
using MoonPioneerClone.ItemsPlacement.Core.Area;
using MoonPioneerClone.ItemsPlacementsInteractions.StackZoneLogic;
using MoonPioneerClone.SetupSystem.StackZones.Markers;
using MoonPioneerClone.Utility;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.StackZones.ComponentsBuilding
{
    public class StackZoneComponentsBuildController
    {
        private readonly GameObject _rootGameObject;

        private readonly IStackZoneComponentsBuilder _interactionsWithOthersBuilder;
        private readonly IStackZoneComponentsBuilder _interactionsWithPlayerBuilder;
        private readonly IStackZoneComponentsBuilder _colliderBuilder;
        private readonly IStackZoneComponentsBuilder _upgradingBuilder;
        
        private StackZoneSetupData _data;
        private StackZone _zone;

        private readonly List<Action> _actions = new List<Action>();

        private Dictionary<Action, string[]> _actionsToChangedPropertiesMap;

        private IEnumerable<string> AlwaysCheck => new []
        {
            nameof(_data.Interactions),
            nameof(_data.PlayerInteractionsSO),
            nameof(_data.ColliderData),
            nameof(_data.UpgradesChain),
        };


        public StackZoneComponentsBuildController(GameObject rootGameObject, SetupScheme zoneSchemePrefab)
        {
            _rootGameObject = rootGameObject;

            _interactionsWithOthersBuilder = new StackZoneInteractionsWithOthersComponentsBuilder(_rootGameObject, zoneSchemePrefab);
            _interactionsWithPlayerBuilder = new StackZoneInteractionWithPlayerComponentsBuilder(_rootGameObject, zoneSchemePrefab);
            _colliderBuilder = new StackZoneMainColliderBuilder(_rootGameObject, zoneSchemePrefab);
            _upgradingBuilder = new StackZoneUpgradingComponentsBuilder(_rootGameObject, zoneSchemePrefab);
            
            InitializeActionsMap();
        }


        private void InitializeActionsMap()
        {
            _actionsToChangedPropertiesMap = new Dictionary<Action, string[]>
            {
                [SetupStackZone] = new[]
                {
                    nameof(_data.PlacementSettings),
                    nameof(_data.AcceptableItems),
                },
                [BuildInteractionsWithOthers] = new[]
                {
                    nameof(_data.InteractWithOthers),
                    nameof(_data.Interactions),
                },
                [BuildInteractionWithPlayer] = new[]
                {
                    nameof(_data.InteractWithPlayer),
                    nameof(_data.PlayerInteractionsSO),
                    nameof(_data.InteractionWithPlayerType),
                },
                [BuildCollider] = new []
                {
                    nameof(_data.ColliderData),
                },
                [BuildUpgrading] = new []
                {
                    nameof(_data.UpgradeableOnItsOwn),
                    nameof(_data.UpgradesChain),
                    nameof(_data.UpgraderColliderRadius),
                },
            };
        }


        public void Build(StackZoneSetupData data, List<string> changedProperties)
        {
            _data = data;
            
            ScheduleComponentsModification(changedProperties);
            ModifyComponents();
        }


        private void ScheduleComponentsModification(List<string> changedProperties)
        {
            changedProperties = changedProperties.Union(AlwaysCheck).ToList();

            foreach (string changedProp in changedProperties)
            {
                Action action = _actionsToChangedPropertiesMap.FirstOrDefault(kvp => kvp.Value.Contains(changedProp)).Key;

                if (!_actions.Contains(action))
                {
                    _actions.Add(action);
                }
            }
        }


        private void ModifyComponents()
        {
            foreach (Action action in _actions)
            {
                action?.Invoke();
            }

            _actions.Clear();
        }


        private void SetupStackZone()
        {
            GameObject objForStackZone = _rootGameObject.GetGameObjectByMarker(typeof(PlacementSetupMarker));
            objForStackZone.GetComponent<PlacementArea>().Setup(_data.PlacementSettings);
            objForStackZone.GetComponent<PlacementAreaDrawer>();
            
            _zone = objForStackZone.GetComponent<StackZone>();
            _zone.Setup(_data.AcceptableItems);
        }


        private void BuildInteractionsWithOthers()
        {
            _interactionsWithOthersBuilder?.Build(_data, _zone);
        }


        private void BuildInteractionWithPlayer()
        {
            if (!_data.InteractWithPlayer)
            {
                return;
            }
            
            _interactionsWithPlayerBuilder?.Build(_data, _zone);
        }


        private void BuildCollider()
        {
            _colliderBuilder?.Build(_data, _zone);
        }


        private void BuildUpgrading()
        {
            if (!_data.UpgradeableOnItsOwn)
            {
                return;
            }
            
            _upgradingBuilder?.Build(_data, _zone);
        }


        public void Clear()
        {
            _data = default;
            _zone = default;

            _actions.Clear();
        }
    }
}