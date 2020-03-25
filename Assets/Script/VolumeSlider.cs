using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("AudioVolume")) {
			float audioVolume = PlayerPrefs.GetFloat ("AudioVolume");
			GetComponent < Slider > ().value = audioVolume;
		} 
		else {
			float audioVolume = 1.0f;
			PlayerPrefs.SetFloat ("AudioVolume", audioVolume);
			GetComponent < Slider > ().value = audioVolume;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
