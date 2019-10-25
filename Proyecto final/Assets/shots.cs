using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shots : MonoBehaviour {

	private Mesh mesh;
	public GameObject shotPrefab;
	public int shotSpeed = 2000;

	//tamaño de bala: (0.001, 0.001, 0.01)
	//posición de salida: posición de blaster + tamaño blaster(4.1) + tamaño bala (0.6)

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)) {
			Debug.Log(new Vector3(transform.position.x, transform.position.y, transform.position.z + mesh.bounds.size.z + 0.6f));
			GameObject shot = Instantiate(shotPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
			
			Rigidbody shotRb = shot.AddComponent<Rigidbody>();
			shotRb.rotation = transform.rotation;
			shotRb.useGravity = false;
			shotRb.AddRelativeForce(new Vector3(0,0,shotSpeed));
		}
	}
}
