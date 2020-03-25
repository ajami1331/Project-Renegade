using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedLevel : MonoBehaviour {

	// Use this for initialization
	int levelUnlocked;
	public int levelIndex;
	public GameObject textObject;
	public Text text;

	void Start () {
		levelUnlocked = PlayerPrefs.GetInt ("LevelUnlocked");
		text = textObject.GetComponent < Text > ();
		if (levelIndex > levelUnlocked) {
			text.text = "Locked";
		}
		else {
			text.text = "";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
