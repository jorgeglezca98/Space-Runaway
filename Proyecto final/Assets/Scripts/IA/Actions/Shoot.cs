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

            if (isCoolingDown)
            {
                Debug.Log("Enemy is cooling down");
                if (Math.Abs(coolDownStart - Time.time) >= MaxOverheatPenalizationTime)
                {
                    Debug.Log("Enemy is NO LONGER COOLING DOWN");
                    isCoolingDown = false;
                    OverheatData.setOverheat(0f);
                    return Status.BH_FAILURE;
                }
                else
                {
                    Debug.Log("Enemy is STILL cooling");
                    return Status.BH_RUNNING;
                }
            }
            else
            {
                Debug.Log("Enemy is OKEY");
                if (isOverheated)
                {
                    Debug.Log("Enemy is OVERHEATED");
                    isCoolingDown = true;
                    coolDownStart = Time.time;
                    return Status.BH_RUNNING;
                }
                else if (isInRange && collidesWithSomething && targetIsHit)
                {
                    Debug.Log("Enemy SHOOOOOOOOOOOOOOOOOT");
                    foreach (Transform blaster in blasters)
                    {
                        Transform shotPoint = blaster.Find("ShotPoint");
                        blaster.rotation = Quaternion.LookRotation(hit.point - shotPoint.position, Vector3.up);
                        GameObject bullet = UnityEngine.Object.Instantiate(ShotPrefab, shotPoint.position, blaster.rotation);
                        bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, ShotSpeed));
                        ArtificialIntelligence.AudioManager.PlaySoundEffect("EnemyShot");
                    }
                    OverheatData.setOverheat(OverheatData.getOverheat() + OverheatIncrement);
                    return Status.BH_SUCCESS;
                }
            }

            Debug.Log("Cant shoot: " + isInRange + ", " + collidesWithSomething + ", " + targetIsHit);
            return Status.BH_FAILURE;
        }

        void coolDown()
        {
            coolDownStart = Time.time;
        }
    }
}
