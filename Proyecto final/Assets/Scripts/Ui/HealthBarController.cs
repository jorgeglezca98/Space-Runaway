using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Slider healthBar;

    private void Awake()
    {
        Sprite healthImage = Resources.Load<Sprite>("S_ItemLightOutline_HeartRed_00");
        GameObject heartIcon = GameObject.Find("HeartIcon");
        heartIcon.GetComponent<Image>().sprite = healthImage;
    }

    private void Start()
    {
        healthBar = gameObject.GetComponent<Slider>();
        healthBar.maxValue = GameObject.Find("PlayerSpaceship").GetComponent<PlayerDestructionController>().Stats.GetMaxHealth();
        healthBar.value = GameObject.Find("PlayerSpaceship").GetComponent<PlayerDestructionController>().Stats.GetMaxHealth();
        GameEventsController.eventController.OnHealthPctChanged += ChangeHealth;
    }

    private void ChangeHealth(float health)
    {
        healthBar.value = health;
    }
}
