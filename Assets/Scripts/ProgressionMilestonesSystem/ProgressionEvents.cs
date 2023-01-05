using System;
using System.Collections.Generic;
using System.Linq;
using ColonizationMobileGame.Utility;
using ColonizationMobileGame.Utility.Helpers;
using UnityEngine;

namespace ColonizationMobileGame.ProgressionMilestonesSystem
{
    public class ProgressionEvents : MonoBehaviour
    {
        private readonly Dictionary<ProgressionMilestoneType, Action> _progressionEvents =
            EnumHelpers.EnumToDictionary<ProgressionMilestoneType, Action>(default(Action));
        
        private readonly Dictionary<ProgressionMilestoneType, bool> _achievedMilestones =
            EnumHelpers.EnumToDictionary<ProgressionMilestoneType, bool>(false);

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
            _achievedMilestones[type] = true;
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


        public bool Achieved(ProgressionMilestoneType milestoneType)
        {
            return _achievedMilestones[milestoneType];
        }
    }
}