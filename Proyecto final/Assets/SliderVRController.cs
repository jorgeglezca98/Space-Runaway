using System.Collections;
using System.Collections.Generic;
using UnityEngine;﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderVRController : MonoBehaviour {

			private bool IsActive;
			private Transform Reticle;
			private float PreviousX;
			private Slider Slider;
			private float ChangeAmount;
			private AudioManager AudioManager;

			void Start () {
					IsActive = false;
					ChangeAmount = 0.1f;
					AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
					Reticle = GameObject.Find("Main Camera").transform;
					Slider = GetComponent<Slider>();
					PreviousX = Reticle.position.x;
					SetInitialVolume();
			}

			void SetInitialVolume(){
				if(gameObject.name == "MusicVolumeSlider"){
					Slider.value = AudioManager.GetMusicVolume();
				}else{
					Slider.value = AudioManager.GetSoundEffectsVolume();
				}
			}

			// void Update () {
			// 		if (IsActive == true && Input.GetButton("Click")) {
			// 			Vector3 currentPosition = Reticle.position;
			// 			float directionOfChange = currentPosition.x - PreviousX;
			// 			ChangeSliderValue(ChangeAmount * directionOfChange);
			// 		} else PreviousX = Reticle.position.x;
			// }


			void ChangeSliderValue(){
				if(gameObject.name == "MusicVolumeSlider")
					AudioManager.ChangeMusicVolume(Slider.value);
				else AudioManager.ChangeSoundEffectSVolume(Slider.value);
			}

			public void TurnVolumeUp(){
				if(Slider.value < Slider.maxValue){
					Slider.value += ChangeAmount;
					ChangeSliderValue();
				}
			}

			public void TurnVolumeDown(){
				if(Slider.value > Slider.minValue){
					Slider.value -= ChangeAmount;
					ChangeSliderValue();
				}
			}

			public void Activate(){
					IsActive = true;
			}

			public void Deactivate(){
					IsActive = false;
			}


}
