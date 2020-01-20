using UnityEngine;

public class AsteroidsMovement : MonoBehaviour
{
    private void Start()
    {
        Rigidbody rg = GetComponent<Rigidbody>();
        System.Random random = new System.Random();

        rg.AddForce(new Vector3(random.Next(-10, 11), random.Next(-10, 11), random.Next(-10, 11)), ForceMode.Impulse);
        rg.AddTorque(new Vector3(random.Next(-200, 201), random.Next(-200, 201), random.Next(-200, 201)), ForceMode.Impulse);
        rg.transform.localScale = Vector3.one * random.Next(5, 51);
    }
}
