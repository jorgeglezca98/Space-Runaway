using System.Collections;
using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    private float maxSpotAngle = 30f;
    private float minSpotAngle = 5f;
    private float maxRange = 1000f;
    private float minRange = 600f;
    private float timeToReachValues = 0.5f;

    // Use this for initialization
    private void Start()
    {
        GameEventsController.eventController.OnAttackModeEnter += InAttackMode;
        GameEventsController.eventController.OnAttackModeExit += OutAttackMode;
    }
    
    private void InAttackMode()
    {
        StartCoroutine("inAttackModeRoutine");
    }

    private void OutAttackMode()
    {
        StartCoroutine("outAttackModeRoutine");
    }

    private IEnumerator InAttackModeRoutine()
    {
        float initialTime = Time.time;
        float endTime = initialTime + timeToReachValues;

        while (Time.time < endTime)
        {
            float pctg = (Time.time - initialTime) / (endTime - initialTime);
            GetComponent<Light>().spotAngle = maxSpotAngle - ((maxSpotAngle - minSpotAngle) * pctg);
            GetComponent<Light>().range = minRange + ((maxRange - minRange) * pctg);
            GetComponent<Light>().color = new Color(1, (1 - pctg), (1 - pctg), 1);
            yield return null;
        }
    }

    private IEnumerator OutAttackModeRoutine()
    {
        float initialTime = Time.time;
        float endTime = initialTime + timeToReachValues;

        while (Time.time < endTime)
        {
            float pctg = (Time.time - initialTime) / (endTime - initialTime);
            GetComponent<Light>().spotAngle = minSpotAngle + ((maxSpotAngle - minSpotAngle) * pctg);
            GetComponent<Light>().range = maxRange - ((maxRange - minRange) * pctg);
            GetComponent<Light>().color = new Color(1, pctg, pctg, 1);
            yield return null;
        }
    }
}
