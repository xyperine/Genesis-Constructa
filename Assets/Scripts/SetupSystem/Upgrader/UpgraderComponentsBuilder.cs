using System;
using ColonizationMobileGame.ItemsPlacementsInteractions;
using UnityEngine;

namespace ColonizationMobileGame.SetupSystem.Upgrader
{
    [Serializable]
    public abstract class UpgraderComponentsBuilder<TSetupData>
        where TSetupData : UpgraderSetupData
    {
        [SerializeField] protected ItemsConsumer consumer;

        [SerializeField, HideInInspector] protected TSetupData setupData;


        public void SetData(TSetupData setupData)
        {
            this.setupData = setupData;
        }
        

        public void Build()
        {
            Setup();
        }


        protected void Setup()
        {
            SetupItemsConsumer();

            SetupUpgrader();
        }
        

        protected abstract void SetupItemsConsumer();


        protected abstract void SetupUpgrader();
    }
}