using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeactivateColliders : MonoBehaviour
{
    private List<GameObject> listOfChildren = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        GetChildRecursive(gameObject);
       foreach(var child in listOfChildren)
        {
            Destroy(child);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetChildRecursive(GameObject obj)
    {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            //child.gameobject contains the current child you can do whatever you want like add it to an array
            if (child.name.Contains("Collider"))
            {
                listOfChildren.Add(child.gameObject);
            }
            GetChildRecursive(child.gameObject);
        }
    }
}
