using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{

    class ReduceOverheat : LeafNode
    {
        OverheatStats overheatData;

        public ReduceOverheat(GameObject agent, OverheatStats overheatData) : base(agent)
        {
            this.overheatData = overheatData;
        }

        public override Status Update()
        {
            //Debug.Log("Reducing overheat");
            overheatData.setOverheat(overheatData.getOverheat() - overheatData.overheatDecrement);
            return Status.BH_SUCCESS;
        }

    }
}
