using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : MonoBehaviour {

    bool attackMode;
    private int spotAngleVariationVelocity = 50;
    private int rangeVariationVelocity = 200;

    // Use this for initialization
    void Start () {
        attackMode = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            attackMode = !attackMode;
        }

        if (attackMode)
        {
            if(GetComponent<Light>().spotAngle > 5) {
                GetComponent<Light>().range += Time.deltaTime * rangeVariationVelocity;
                // GetComponent<Light>().color += new Color(0.1f, 0f, 0f, 1f);
                GetComponent<Light>().color += Color.red / 5.0f;
                GetComponent<Light>().spotAngle -= Time.deltaTime * spotAngleVariationVelocity;
            }
        }else{
            if (GetComponent<Light>().spotAngle < 30){
                GetComponent<Light>().range -= Time.deltaTime * rangeVariationVelocity;
              //  GetComponent<Light>().color -= new Color(0.1f, 0f, 0f, 1f);
                GetComponent<Light>().color -= Color.red / 5.0f;
                GetComponent<Light>().spotAngle += Time.deltaTime * spotAngleVariationVelocity;
            }
        }

    }
}
