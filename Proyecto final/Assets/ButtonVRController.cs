using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonVRController : MonoBehaviour {

			private bool IsActive;
			private Button Button;

			void Start () {
					IsActive = false;
					Button = GetComponent<Button>();
			}

			void Update () {
					if (IsActive == true && Input.GetButtonDown("Click")) {
							Button.onClick.Invoke();
							IsActive = false;
					}
			}

			public void Activate(){
				Debug.Log("I'm activating!");
					IsActive = true;
			}

			public void Deactivate(){
				Debug.Log("I'm deactivating!");
					IsActive = false;
			}


}
