using System.Collections;
using UnityEngine;

public class Shots : MonoBehaviour
{

    private GameObject shotPrefab;
    private AudioManager audioManager;
    private int shotSpeed = 10000;
    private float shotMaxDistance = 100f;
    private float shotMinDistance = 15f; // If it is less than 10 it could be problematic
    private float rangeSize = 15f;
    private Transform shotPoint;
    private GameObject lastShot;
    private float lastShotSize;
    private OverheatStats stats = new OverheatStats();

    // Time since the last time the player attacked.
    private float attackModeTimer = 0.0f;
    // Maximum time the player is in attack mode
    // after he stops attacking (in seconds).
    private float maxAttackModeTimer = 2f;

    // The amount of overheat the weapon produces everytime it shots.
    private float overheatIncrement = 4f;
    // The amount of overheat the weapon cools down everytime it shots.
    private float overheatDecrement = 2f;
    // The amount of time in seconds the weapon get disabled when the
    // maximum overheat is achieved.
    private float maxOverheatPenalization = 3f;

    private bool isCoolingDown = false;

    public int bulletsShot;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        shotPrefab = Resources.Load("player_shot_prefab") as GameObject;
        GameEventsController.eventController.OnMaximumOverheat += CoolDown;

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

    private void FixedUpdate()
    {
        if (stats.GetOverheat() < stats.GetMaxOverheat())
        {
            if (Input.GetButton("Shoot") && (lastShot == null || Vector3.Distance(shotPoint.position, lastShot.transform.position - new Vector3(0, 0, lastShotSize / 2)) > lastShotSize))
            {
                RaycastHit hit;
                GameObject hud = GameObject.FindWithTag("playerHUD");

                RaycastHit[] hitsInRange = Physics.BoxCastAll(
                    hud.transform.position,
                    new Vector3(rangeSize, rangeSize, shotMinDistance),
                    transform.TransformDirection(Vector3.forward),
                    hud.transform.rotation,
                    shotMaxDistance,
                    (1 << 10));

                if (hitsInRange.Length > 0)
                {
                    int i = 0;
                    Vector3 origenRaycast = hud.transform.position;
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
                        float enemyWingspan;

                        if (hit.transform.name == "AssaultEnemy(Clone)")
                        {
                            enemyWingspan = hit.transform.GetComponent<AssaultArtificialIntelligence>().GetSpaceshipDimension().x;
                        }
                        else
                        {
                            enemyWingspan = hit.transform.GetComponent<KamikazeArtificialIntelligence>().GetSpaceshipDimension().x;
                        }

                        Vector3 enemyFuturePosition = hit.transform.position + hit.transform.GetComponent<Rigidbody>().velocity;
                        transform.rotation = Quaternion.LookRotation(enemyFuturePosition - shotPoint.position, Vector3.up);
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

                bulletsShot++;

                lastShot = shot;
                lastShotSize = lastShot.GetComponent<Renderer>().bounds.size.z;

                if (!stats.IsAttacking())
                {
                    stats.SetAttackMode(true);
                    GameEventsController.eventController.AttackModeEnter();
                }

                attackModeTimer = Time.time + maxAttackModeTimer;
                //Stats.setOverheat(Stats.getOverheat() + overheatIncrement);
                ChangeOverheat(stats.GetOverheat() + overheatIncrement);
                audioManager.PlaySoundEffect("PlayerShot");

            }
        }
        else if (!isCoolingDown)
        {
            isCoolingDown = true;
            CoolDown();
        }
    }

    private void ChangeOverheat(float amount)
    {
        stats.SetOverheat(amount);
        GameEventsController.eventController.OverheatPctChanged(stats.GetOverheat());
    }

    private void Update()
    {
        ManageAttackMode();
        if (stats.GetOverheat() < stats.GetMaxOverheat() && stats.GetOverheat() > 0)
        {
            ChangeOverheat(stats.GetOverheat() - overheatDecrement);
            //Stats.setOverheat(Stats.getOverheat() - overheatDecrement);
        }
    }

    private void CoolDown()
    {
        StartCoroutine("coolDown_");
    }

    private IEnumerator CoolDown_()
    {
        yield return new WaitForSeconds(maxOverheatPenalization);
        ChangeOverheat(0f);
        isCoolingDown = false;
        //Stats.setOverheat(0f);
    }

    // When the time in "maxAttackModeTimer" has passed
    // the attack mode sets to false.
    private void ManageAttackMode()
    {
        if (stats.IsAttacking())
        {
            if (attackModeTimer <= Time.time)
            {
                GameEventsController.eventController.AttackModeExit();
                stats.SetAttackMode(false);
            }
        }
    }

    // void OnDrawGizmos()
    //   {
    //   	GameObject HUD = GameObject.FindWithTag("playerHUD");
    //       ExtDebug.DrawBoxCastBox(
    //       	HUD.transform.position,
    // 		new Vector3(RangeSize, RangeSize, ShotMinDistance),
    // 		HUD.transform.rotation,
    // 		transform.TransformDirection(Vector3.forward),
    // 		ShotMaxDistance);
    //   }

}
