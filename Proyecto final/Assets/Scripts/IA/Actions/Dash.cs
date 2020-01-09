using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//namespace BehaviorTree
//{
class Dash : MonoBehaviour
{
    //public Dash(GameObject agent, int speed) : base(agent)
    //{
    //    this.speed = speed;
    //}
    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.B))
        {
            GetComponent<Rigidbody>().AddRelativeForce(new Vector3(-Time.deltaTime * 350, 0, 0), ForceMode.Impulse);
        }
        //return Status.BH_SUCCESS;
    }
}
//}
