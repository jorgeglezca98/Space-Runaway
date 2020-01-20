using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Image ForegroundImage { get; set; }
    private float updateSpeedSeconds = 0.5f;

    private void Awake()
    {
        GameEventsController.eventController.OnHealthPctChanged += HandleHealthChanged;
    }

    // This function is called when the health is modified
    // we start a co-routine to soft the effect of the health change.
    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    // We're substracting to the foreground image a little of the
    // health percentage that has changed each time.
    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = ForegroundImage.fillAmount;
        float elapsed = 0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            ForegroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
        ForegroundImage.fillAmount = pct;
    }
}
