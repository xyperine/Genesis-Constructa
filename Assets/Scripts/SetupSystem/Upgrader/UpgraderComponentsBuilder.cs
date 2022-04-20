using System;
using MoonPioneerClone.ItemsPlacementsInteractions;
using UnityEngine;

namespace MoonPioneerClone.SetupSystem.Upgrader
{
    [Serializable]
    public abstract class UpgraderComponentsBuilder<TSetupData>
        where TSetupData : UpgraderSetupData
    {
        [SerializeField] protected ItemsConsumer consumer;
        [SerializeField] protected SphereCollider collider;

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


        public abstract void SetupCollider();


        protected abstract void SetupItemsConsumer();


        protected abstract void SetupUpgrader();
    }
}