using UnityEngine;
using UnityEngine.UI;

public class OverheatBarController : MonoBehaviour
{
    // Use this for initialization
    private Slider overheatBar;

    private void Awake()
    {
        Sprite overheatSprite = Resources.Load<Sprite>("S_ItemLightOutline_JarPurple_00");
        GameObject overheatIcon = GameObject.Find("OverheatIcon");
        overheatIcon.GetComponent<Image>().sprite = overheatSprite;
    }


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
