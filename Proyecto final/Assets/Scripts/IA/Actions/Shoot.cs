using UnityEngine;

namespace BehaviorTree
{
    public class Shoot : LeafNode
    {
        private GameObject shotPrefab;
        private int shotSpeed;

        public Shoot(GameObject agent, GameObject shotPrefab, int shotSpeed) : base(agent)
        {
            this.shotPrefab = shotPrefab;
            this.shotSpeed = shotSpeed;
        }

        public override Status Update()
        {
            Transform[] blasters = new Transform[2] { agent.transform.Find("Blaster-1"), agent.transform.Find("Blaster-2") };

            foreach (Transform blaster in blasters)
            {
                Transform shotPoint = blaster.Find("ShotPoint");
                blaster.rotation = Quaternion.LookRotation(ArtificialIntelligence.target.transform.position - shotPoint.position, Vector3.up);
                GameObject bullet = UnityEngine.Object.Instantiate(shotPrefab, shotPoint.position, blaster.rotation);
                bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, shotSpeed));
                ArtificialIntelligence.audioManager.PlaySoundEffect("EnemyShot");
            }

            //Debug.Log("SHOOOOOOOOOOOOOOOOOOOT");
            return Status.BH_SUCCESS;
        }
    }
}
