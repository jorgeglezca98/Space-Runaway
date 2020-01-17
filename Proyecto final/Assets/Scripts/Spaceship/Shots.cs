using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shots : MonoBehaviour
{

    private GameObject shotPrefab;
    private AudioManager AudioManager;
    private int shotSpeed = 10000;
    private float ShotMaxDistance = 100f;
    private float ShotMinDistance = 15f; // If it is less than 10 it could be problematic
    private float RangeSize = 10f;
    private Transform shotPoint;
    private GameObject lastShot;
    private float lastShotSize;
    private OverheatStats Stats = new OverheatStats();

    // Time since the last time the player attacked.
    private float attackModeTimer = 0.0f;
    // Maximum time the player is in attack mode
    // after he stops attacking (in seconds).
    private float maxAttackModeTimer = 2f;

    // The amount of overheat the weapon produces everytime it shots.
    private float overheatIncrement = 5f;
    // The amount of overheat the weapon cools down everytime it shots.
    private float overheatDecrement = 2f;
    // The amount of time in seconds the weapon get disabled when the
    // maximum overheat is achieved.
    private float maxOverheatPenalization = 5f;

    private bool isCoolingDown = false;

    // Use this for initialization
    void Start()
    {

        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        shotPrefab = Resources.Load("player_shot_prefab") as GameObject;


        GameEventsController.eventController.OnMaximumOverheat += coolDown;

        shotPoint = null;
        foreach (Transform child in transform)
        {
            if (child.tag == "shotPoint")
            {
                shotPoint = child;
                break;
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Stats.getOverheat() < Stats.getMaxOverheat())
        {
            if (Input.GetButton("Shoot") && (lastShot == null || Vector3.Distance(shotPoint.position, lastShot.transform.position - new Vector3(0, 0, lastShotSize / 2)) > lastShotSize))
            {
                RaycastHit hit;
                GameObject HUD = GameObject.FindWithTag("playerHUD");

                RaycastHit[] hitsInRange = Physics.BoxCastAll(
                    HUD.transform.position,
                    new Vector3(RangeSize, RangeSize, ShotMinDistance),
                    transform.TransformDirection(Vector3.forward),
                    Quaternion.identity,
                    ShotMaxDistance,
                    (1 << 10));

                if (hitsInRange.Length > 0)
                {
                    int i = 0;
                    Vector3 origenRaycast = HUD.transform.position;
                    do
                    {
                        Physics.Raycast(
                            origenRaycast,
                            hitsInRange[i].transform.position - origenRaycast,
                            out hit,
                            Mathf.Infinity,
                            ~(1 << 8) & ~(1 << 9));
                        i++;
                    } while (hit.transform.tag != "enemy" && i < hitsInRange.Length);

                    if (hit.transform.tag == "enemy")
                    {
                        transform.rotation = Quaternion.LookRotation(hit.point - shotPoint.position, Vector3.up);
                    }
                    else
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

                GameObject shot = Instantiate(shotPrefab, shotPoint.position, transform.rotation);

                Rigidbody shotRb = shot.GetComponent<Rigidbody>();
                shotRb.AddRelativeForce(new Vector3(0, 0, shotSpeed));

                lastShot = shot;
                lastShotSize = lastShot.GetComponent<Renderer>().bounds.size.z;

                if (!Stats.isAttacking())
                {
                    Stats.setAttackMode(true);
                    GameEventsController.eventController.attackModeEnter();
                }

                attackModeTimer = Time.time + maxAttackModeTimer;
                //Stats.setOverheat(Stats.getOverheat() + overheatIncrement);
                ChangeOverheat(Stats.getOverheat() + overheatIncrement);
                AudioManager.PlaySoundEffect("PlayerShot");

            }
        }
        else if (!isCoolingDown)
        {
            isCoolingDown = true;
            coolDown();
        }
    }

    void ChangeOverheat(float amount)
    {
        Stats.setOverheat(amount);
        GameEventsController.eventController.overheatPctChanged(Stats.getOverheat());
    }

    void Update()
    {
        manageAttackMode();
        if (Stats.getOverheat() < Stats.getMaxOverheat() && Stats.getOverheat() > 0)
        {
            ChangeOverheat(Stats.getOverheat() - overheatDecrement);
            //Stats.setOverheat(Stats.getOverheat() - overheatDecrement);
        }
    }

    void coolDown()
    {
        StartCoroutine("coolDown_");
    }

    IEnumerator coolDown_()
    {
        yield return new WaitForSeconds(maxOverheatPenalization);
        ChangeOverheat(0f);
        isCoolingDown = false;
        //Stats.setOverheat(0f);
    }

    // When the time in "maxAttackModeTimer" has passed
    // the attack mode sets to false.
    void manageAttackMode()
    {
        if (Stats.isAttacking())
        {
            if (attackModeTimer <= Time.time)
            {
                GameEventsController.eventController.attackModeExit();
                Stats.setAttackMode(false);
            }
        }
    }

    void OnDrawGizmos()
    {
        GameObject HUD = GameObject.FindWithTag("playerHUD");
        ExtDebug.DrawBoxCastBox(
            HUD.transform.position,
            new Vector3(RangeSize, RangeSize, ShotMinDistance),
            Quaternion.identity,
            transform.TransformDirection(Vector3.forward),
            ShotMaxDistance);
    }

}
