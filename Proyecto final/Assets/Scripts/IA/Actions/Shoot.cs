using UnityEngine;

namespace BehaviorTree {

	class Shoot : LeafNode {

		public GameObject ShotPrefab;
		public int ShotMaxDistance;
		public int ShotMinDistance;
		public int ShotSpeed;
		public int RangeSize;

		public Shoot(GameObject agent, GameObject shotPrefab, int shotMaxDistance, 
					    int shotSpeed, int rangeSize, int shotMinDistance) : base(agent) {
			ShotPrefab = shotPrefab;
			ShotMaxDistance = shotMaxDistance;
			ShotSpeed = shotSpeed;
			RangeSize = rangeSize;
			ShotMinDistance = (shotMinDistance < 10) ? 10 : shotMinDistance;
		}

	    public override Status Update() {
			RaycastHit hit;
			Transform[] blasters = new Transform[2] { Agent.transform.Find("Blaster-1"), Agent.transform.Find("Blaster-2") };

			if (Physics.BoxCast(Agent.transform.position, new Vector3(RangeSize, RangeSize, ShotMinDistance), 
				Agent.transform.TransformDirection(Vector3.forward), out hit, Quaternion.identity, ShotMaxDistance, ~(1 << 8)))
            {
            	if(GameObject.ReferenceEquals(ArtificialIntelligence.Target, hit.transform.gameObject)) {
            		foreach(Transform blaster in blasters) {
            			Transform shotPoint = blaster.Find("ShotPoint");
            			blaster.rotation = Quaternion.LookRotation(hit.point - shotPoint.position, Vector3.up);
            			GameObject bullet = Object.Instantiate(ShotPrefab, shotPoint.position, blaster.rotation);
			            bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, ShotSpeed));
            		}
            		
                	return Status.BH_SUCCESS;
                } else {
                	return Status.BH_FAILURE;
                }
            } else {
            	return Status.BH_FAILURE;
            }
	    }
	}
}