using UnityEngine;
using UnityEngine.UI;


public class ButtonVRController : MonoBehaviour
{

    private bool isActive;
    private Button button;

    private void Start()
    {
        isActive = false;
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (isActive == true && Input.GetButtonDown("Click"))
        {
            button.onClick.Invoke();
            isActive = false;
        }
    }

    public void Activate()
    {
        Debug.Log("I'm activating!");
        isActive = true;
    }

    public void Deactivate()
    {
        Debug.Log("I'm deactivating!");
        isActive = false;
    }


}
