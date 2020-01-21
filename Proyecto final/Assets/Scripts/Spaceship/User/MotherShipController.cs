using UnityEngine;

public class MotherShipController : MonoBehaviour
{
    private void Awake()
    {
        Sprite motherShipSprite = Resources.Load<Sprite>("T_17_medal_star_");
        GameObject motherShipIcon = GameObject.Find("MotherShipIcon");
        motherShipIcon.GetComponent<SpriteRenderer>().sprite = motherShipSprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            GameEventsController.eventController.PlayerWon();
        }
    }
}
