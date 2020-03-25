using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookSlider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("MouseSensitivty")) {
			float mouseSensitivty = PlayerPrefs.GetFloat ("MouseSensitivty");
			GetComponent < Slider > ().value = mouseSensitivty;
		} else {
			float mouseSensitivty = 2.5f;
			PlayerPrefs.SetFloat ("MouseSensitivty", mouseSensitivty );
			GetComponent < Slider > ().value = mouseSensitivty;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
