using UnityEngine;

internal class MotherShipIndicator : MonoBehaviour
{

    private GameObject Pointer;
    private GameObject MotherShip;
    private float Distance = 3;

    public void Start()
    {
        Pointer = GameObject.Find("MotherShipIcon");
        MotherShip = GameObject.Find("MotherShip");
    }

    public void LateUpdate()
    {
        Pointer.transform.position = transform.position + Distance * (MotherShip.transform.position - transform.position).normalized;
        Pointer.transform.LookAt(transform);
    }
}
