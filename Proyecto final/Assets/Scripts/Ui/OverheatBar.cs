using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OverheatBar : MonoBehaviour
{
    private float updateSpeedSeconds = 0.5f;

    public Image ForegroundImage { get; set; }

    private void Start()
    {
        GameEventsController.eventController.OnOverheatPctChanged += HandleOverheatChanged;
    }

    // This function is called when the overheat is modified
    // we start a co-routine to soft the effect of the overheat change.
    private void HandleOverheatChanged(float pct)
    {
        Debug.Log("Im here!\n");
        StartCoroutine(ChangeToPct(pct));
    }

    // We're substracting to the foreground image a little of the
    // overheat percentage that has changed each time.
    private IEnumerator ChangeToPct(float pct)
    {
        Debug.Log(pct);
        float preChangePct = ForegroundImage.fillAmount;
        float elapsed = 0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            if (pct == 0)    // Only when the overheat is zero we want to soft the overheat bar fill changing.
            {
                ForegroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            }
            else
            {
                ForegroundImage.fillAmount = pct;
            }

            yield return null;
        }
        ForegroundImage.fillAmount = pct;
    }
}
