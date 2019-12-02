using UnityEngine;

namespace BehaviorTree {

	class Disparar : LeafNode {

		public Disparar(GameObject agent) : base(agent) {}

	    public override Status Update() {
			RaycastHit hit;

			if (Physics.BoxCast(Agent.GetComponent<Renderer>().bounds.center, , Agent.transform.TransformDirection(Vector3.forward), 
				out hit, , shotMaxDistance, ~(1 << 8)))
            {
            	if(es el objetivo){
					GameObject shot = Instantiate(shotPrefab, shotPoint.position, transform.rotation);

					Rigidbody shotRb = shot.AddComponent<Rigidbody>();
					shotRb.useGravity = false;
		            shotRb.AddRelativeForce(new Vector3(0,0,shotSpeed));

		            lastShot = shot;
		            lastShotSize = lastShot.GetComponent<Renderer>().bounds.size.z;
                	return Status.BH_SUCCESS;
                } else {
                	return Status.BH_FAILURE;
                }
            } else {
            	return Status.BH_FAILURE;
            }
            return Status.BH_FAILURE;
	    }
	}
}