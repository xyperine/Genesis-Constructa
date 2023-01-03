﻿using System.Linq;
using ColonizationMobileGame.Level;
using ColonizationMobileGame.ScoreSystem;
using ColonizationMobileGame.TasksSystem;
using ColonizationMobileGame.TutorialSystem;
using ColonizationMobileGame.UI.ArrowPointers.Target;
using ColonizationMobileGame.UI.ArrowPointers.Target.Factories;
using UnityEngine;

namespace ColonizationMobileGame.Initialization
{
    public class DependenciesResolver : MonoBehaviour
    {
        [SerializeField] private TasksInitializer tasksInitializer;
        [SerializeField] private TutorialBuilder tutorialBuilder;
        
        private LevelData _levelData;
        private InteractablesTracker _interactablesTracker;
        private ScoreModifier _scoreModifier;
        private ArrowPointersTargetsManager _arrowPointersTargetsManager;

        private Component[] _allComponents;


        public void ResolveBeforeRestoringSave()
        {
            GetObjects();
            
            SetDependencies();
        }


        private void GetObjects()
        {
            _allComponents = FindObjectsOfType<Component>(true);
            
            _levelData = FindObjectOfType<LevelData>();
            _interactablesTracker = FindObjectOfType<InteractablesTracker>();
            _scoreModifier = FindObjectOfType<ScoreModifier>();
            _arrowPointersTargetsManager = FindObjectOfType<ArrowPointersTargetsManager>();
        }


        private void SetDependencies()
        {
            SetInteractablesTracker();

            SetCameraForCanvases(); 
            SetLevelData();

            SetScoreModifier();
        }


        private void SetInteractablesTracker()
        {
            foreach (IInteractablesTrackerUser dataUser in _allComponents.OfType<IInteractablesTrackerUser>())
            {
                dataUser.SetInteractablesTracker(_interactablesTracker);
            }
        }


        private void SetCameraForCanvases()
        {
            foreach (Canvas canvas in _allComponents.OfType<Canvas>())
            {
                canvas.worldCamera = Camera.main;
            }
        }


        private void SetLevelData()
        {
            tasksInitializer.SetLevelData(_levelData);
            
            foreach (ILevelDataUser dataUser in _allComponents.OfType<ILevelDataUser>())
            {
                dataUser.SetLevelData(_levelData);
            }
        }


        private void SetScoreModifier()
        {
            tasksInitializer.SetScoreCounter(_scoreModifier);
        }


        public void ResolveAfterRestoringSave()
        {
            SetArrowPointerTargetFactory();
            
            tutorialBuilder.Completed += SetArrowPointerTargetFactory;
        }


        private void SetArrowPointerTargetFactory()
        {
            TargetsFactory targetsFactory;
            if (tutorialBuilder.Complete)
            {
                targetsFactory = new RegularTargetsFactory();
            }
            else
            {
                targetsFactory = new TutorialTargetsFactory();
            }

            _arrowPointersTargetsManager.SetFactory(targetsFactory);
        }
    }
}