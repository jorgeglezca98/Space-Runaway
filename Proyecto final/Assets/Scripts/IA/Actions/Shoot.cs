using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{

    class Shoot : LeafNode
    {

        public GameObject ShotPrefab;
        public int ShotSpeed;

        public Shoot(GameObject agent, GameObject shotPrefab, int shotSpeed) : base(agent)
        {
            ShotPrefab = shotPrefab;
            ShotSpeed = shotSpeed;
        }

        public override Status Update()
        {

            Transform[] blasters = new Transform[2] { Agent.transform.Find("Blaster-1"), Agent.transform.Find("Blaster-2") };

            foreach (Transform blaster in blasters)
            {
                Transform shotPoint = blaster.Find("ShotPoint");
                blaster.rotation = Quaternion.LookRotation(ArtificialIntelligence.Target.transform.position - shotPoint.position, Vector3.up);
                GameObject bullet = UnityEngine.Object.Instantiate(ShotPrefab, shotPoint.position, blaster.rotation);
                bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, ShotSpeed));
                ArtificialIntelligence.AudioManager.PlaySoundEffect("EnemyShot");
            }
            return Status.BH_SUCCESS;
        }
    }
}
