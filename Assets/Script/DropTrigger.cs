using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrigger : MonoBehaviour {

	public string messege;

	// Use this for initialization
	void Start () {
		if (!String.IsNullOrEmpty ( messege )) {
			GameObject.FindWithTag ("LevelManager").BroadcastMessage( messege );
			Destroy ( gameObject );
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
