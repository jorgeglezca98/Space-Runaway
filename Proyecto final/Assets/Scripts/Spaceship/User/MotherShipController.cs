using UnityEngine;

public class MotherShipController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            GameEventsController.eventController.PlayerWon();
        }
    }
}
