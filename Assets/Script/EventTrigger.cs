using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

	// Use this for initialization

	public GameObject manager;
	public string messege;

	void Start () {
		manager = GameObject.FindWithTag ("LevelManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			manager.BroadcastMessage (messege);
			this.gameObject.SetActive (false);
		}
	}
}
