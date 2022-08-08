using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.ProgressionTracking;
using UnityEngine;

namespace ColonizationMobileGame.ProgressionMilestonesSystem
{
    public class ProgressionEvents : MonoBehaviour
    {
        private readonly Dictionary<ProgressionMilestoneType, Action> _progressionEvents =
            new Dictionary<ProgressionMilestoneType, Action>
            {
                [ProgressionMilestoneType.MetalFactoryBuilt] = default,
                [ProgressionMilestoneType.MineralFactoryUnlocked] = default,
            };

        private IProgressionMilestone[] _milestones;


        private void Awake()
        {
            _milestones = GetComponentsInChildren<IProgressionMilestone>();
        }


        private void OnEnable()
        {
            foreach (IProgressionMilestone milestone in _milestones)
            {
                milestone.Achieved += InvokeProgressionEvent;
            }
        }


        private void InvokeProgressionEvent(ProgressionMilestoneType type)
        {
            _progressionEvents[type]?.Invoke();

            _progressionEvents[type] = default;
        }


        private void OnDisable()
        {
            foreach (IProgressionMilestone milestone in _milestones)
            {
                milestone.Achieved -= InvokeProgressionEvent;
            }
        }


        public void Subscribe(ProgressionMilestoneType type, Action action)
        {
            if (_progressionEvents[type] != null &&
                _progressionEvents[type].GetInvocationList().Contains(action))
            {
                return;
            }
            
            _progressionEvents[type] += action;
        }


        public void Unsubscribe(ProgressionMilestoneType type, Action action)
        {
            if (_progressionEvents[type] == null ||
                !_progressionEvents[type].GetInvocationList().Contains(action))
            {
                return;
            }
            
            _progressionEvents[type] -= action;
        }
    }
}