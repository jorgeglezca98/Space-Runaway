using System;
using System.Collections;
using UnityEngine;

namespace BehaviorTree
{

    class Shoot : LeafNode
    {

        public GameObject ShotPrefab;
        public int ShotMaxDistance;
        public int ShotMinDistance;
        public int ShotSpeed;
        public int RangeSize;

        float OverheatIncrement;
        float OverheatDecrement;
        float MaxOverheatPenalizationTime;
        OverheatStats OverheatData;
        private bool isCoolingDown;

        private float coolDownStart;

        public Shoot(GameObject agent, GameObject shotPrefab, int shotMaxDistance,
                        int shotSpeed, int rangeSize, int shotMinDistance,
                        float overheatIncrement, float overheatDecrement, float maxOverheatPenalizationTime,
                        OverheatStats overheatData) : base(agent)
        {
            ShotPrefab = shotPrefab;
            ShotMaxDistance = shotMaxDistance;
            ShotSpeed = shotSpeed;
            RangeSize = rangeSize;
            ShotMinDistance = (shotMinDistance < 10) ? 10 : shotMinDistance;

            OverheatIncrement = overheatIncrement;
            OverheatDecrement = overheatDecrement;
            MaxOverheatPenalizationTime = maxOverheatPenalizationTime;
            OverheatData = overheatData;

            isCoolingDown = false;
        }

        public override Status Update()
        {
            RaycastHit hit;

            Transform[] blasters = new Transform[2] { Agent.transform.Find("Blaster-1"), Agent.transform.Find("Blaster-2") };

            bool isInRange = Physics.BoxCast(Agent.transform.position, new Vector3(RangeSize, RangeSize, ShotMinDistance),
                Agent.transform.TransformDirection(Vector3.forward), Quaternion.identity, ShotMaxDistance, (1 << 9));
            bool collidesWithSomething = Physics.Raycast(Agent.transform.position,
                ArtificialIntelligence.Target.transform.position - Agent.transform.position, out hit, ShotMaxDistance, ~(1 << 8));
            bool targetIsHit = GameObject.ReferenceEquals(ArtificialIntelligence.Target, hit.transform.gameObject);

            bool isOverheated = OverheatData.getOverheat() >= OverheatData.getMaxOverheat();

            if (isInRange && collidesWithSomething && targetIsHit && !isOverheated)
            {
                foreach (Transform blaster in blasters)
                {
                    Debug.Log("Blaster name" + blaster.gameObject.name);
                    Transform shotPoint = blaster.Find("ShotPoint");
                    blaster.rotation = Quaternion.LookRotation(hit.point - shotPoint.position, Vector3.up);
                    GameObject bullet = UnityEngine.Object.Instantiate(ShotPrefab, shotPoint.position, blaster.rotation);
                    bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, ShotSpeed));
                    ArtificialIntelligence.AudioManager.PlaySoundEffect("EnemyShot");
                }
                Debug.Log("SHOT");
                OverheatData.setOverheat(OverheatData.getOverheat() + OverheatIncrement);
                Debug.Log("Actual overheat: " + OverheatData.getOverheat());
                return Status.BH_SUCCESS;
            }
            else if (!isCoolingDown && isOverheated)
            {
                Debug.Log("COULD NOT SHOOT IS OVERHEATED (isOverheated SAYS " + isOverheated + ")");
                isCoolingDown = true;
                coolDown();
                return Status.BH_RUNNING;
            }
            else if (isCoolingDown)
            {
                Debug.Log("Is cooling Down!");
                if (Math.Abs(coolDownStart - Time.time) >= MaxOverheatPenalizationTime)
                {
                    Debug.Log("Cooling Down now is done!");
                    isCoolingDown = false;
                    OverheatData.setOverheat(0f);
                    return Status.BH_FAILURE;
                }
                else
                    return Status.BH_RUNNING;
            }
            else
            {
                return Status.BH_FAILURE;   
            }
        }

        void coolDown()
        {
            coolDownStart = Time.time;
        }
    }
}
