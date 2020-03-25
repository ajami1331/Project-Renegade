using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("GodMode")) {
			int godMode = PlayerPrefs.GetInt ("GodMode");
			GetComponent < Slider > ().value = godMode;
		} else {
			int godMode = 0;
			PlayerPrefs.SetInt ("GodMode", godMode);
			GetComponent < Slider > ().value = godMode;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
