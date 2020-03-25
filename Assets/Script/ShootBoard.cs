using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBoard : MonoBehaviour {

	// Use this for initialization
	public GameObject playerManager;
	public int health;
	public GameObject destroyedObject;

	void Start () {
		playerManager = GameObject.FindGameObjectWithTag ( "Manager" );
		health = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ApplyDamage( int h ) {
		if (health <= 0) {
			return;
		}	 
		health -= h;
		if (health <= 0) {
			playerManager.BroadcastMessage ( "UpdateKill" );
			if (destroyedObject != null) {
				GameObject body = Instantiate (destroyedObject, this.transform.position, this.transform.rotation);
				body.transform.parent = null;
			}
			Destroy (this.gameObject);
		}
	}
}
