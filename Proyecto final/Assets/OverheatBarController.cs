using UnityEngine;
using UnityEngine.UI;

public class OverheatBarController : MonoBehaviour
{
    // Use this for initialization
    private Slider overheatBar;

    // Use this for initialization
    private void Start()
    {
        overheatBar = gameObject.GetComponent<Slider>();
        GameEventsController.eventController.OnOverheatPctChanged += ChangeOverheat;
    }

    private void ChangeOverheat(float overheat)
    {
        overheatBar.value = overheat;
    }
}
