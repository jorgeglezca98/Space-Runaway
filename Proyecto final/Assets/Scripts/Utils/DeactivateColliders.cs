using System.Collections.Generic;
using UnityEngine;

public class DeactivateColliders : MonoBehaviour
{
    private List<GameObject> listOfChildren = new List<GameObject>();

    private void Start()
    {
        GetChildRecursive(gameObject);
        foreach (GameObject child in listOfChildren)
        {
            Destroy(child);
        }
    }

    private void GetChildRecursive(GameObject obj)
    {
        if (null == obj)
        {
            return;
        }

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            //child.gameobject contains the current child you can do whatever you want like add it to an array
            if (child.name.Contains("Collider"))
            {
                listOfChildren.Add(child.gameObject);
            }
            GetChildRecursive(child.gameObject);
        }
    }
}
