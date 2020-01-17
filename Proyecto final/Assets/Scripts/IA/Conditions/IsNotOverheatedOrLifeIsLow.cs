using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BehaviorTree
{
    class IsNotOverheatedOrLifeIsLow : LeafNode
    {
        float overheatThreshold;
        float healthThreshold;

        float lowThreshold;
        bool waitToShoot = false;

        DestructionController destructionController;
        OverheatStats overheatData;

        public IsNotOverheatedOrLifeIsLow(GameObject agent, DestructionController destructionController, OverheatStats overheatData, float overheatThreshold, float healthThreshold)
            : base(agent)
        {
            this.destructionController = destructionController;
            this.overheatData = overheatData;

            this.overheatThreshold = overheatThreshold;
            this.healthThreshold = healthThreshold;

            this.lowThreshold = /*lowThreshold*/ overheatData.getMaxOverheat() * 0.2f;
        }

        public override Status Update()
        {
            float health = destructionController.Stats.getHealth();
            float overheat = overheatData.getOverheat();

            if (health < healthThreshold)
            {
                return Status.BH_SUCCESS;
            }
            else if (overheat >= overheatThreshold)
            {
                waitToShoot = true;
            }
            else if (overheat <= lowThreshold)
            {
                waitToShoot = false;
                return Status.BH_SUCCESS;
            }
            else if (!waitToShoot)
            {
                return Status.BH_SUCCESS;
            }

            Debug.Log("Should not shoot");

            Debug.Log("Overhead threshold not exceeded: " + (overheat < overheatThreshold));
            Debug.Log("Health threshold exceeded: " + (health < healthThreshold));

            return Status.BH_FAILURE;
        }
    }
}
