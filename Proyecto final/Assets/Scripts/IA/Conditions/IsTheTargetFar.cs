using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BehaviorTree
{

    class IsTheTarjetFar : LeafNode
    {

        private int FarLimit;

        public IsTheTarjetFar(GameObject agent, int farLimit) : base(agent)
        {
            FarLimit = farLimit;
        }

        public override Status Update()
        {
            double distance = (Agent.transform.position - ArtificialIntelligence.Target.transform.position).magnitude;
            if (distance > FarLimit)
            {
                //Debug.Log("The AI name is: " + Agent.transform.name + ", Distance " + distance + " is far from target. SUCCESS");
                return Status.BH_SUCCESS;
            }
            else
            {
                //Debug.Log("The AI name is: " + Agent.transform.name + ", Distance " + distance + " is NOT! far from target. FAILURE");
                return Status.BH_FAILURE;
            }
        }
    }
}