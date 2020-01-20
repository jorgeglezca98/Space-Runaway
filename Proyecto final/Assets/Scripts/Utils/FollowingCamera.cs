using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    // Use this for initialization
    private void Start()
    {
        player = GameObject.Find("Cockpit3_Glass");
        // offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player)
        {
            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
        }
    }
}
