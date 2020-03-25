using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public int health = 100;
	public GameObject destroyedObject;
	public GameObject dropTrigger;
	public string messege;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ApplyDamage( int h ) {
		health -= h;
		if (health <= 0) {
			if (destroyedObject != null) {
				GameObject body = Instantiate (destroyedObject, this.transform.position, this.transform.rotation);
				body.transform.parent = null;
			}
			if (dropTrigger != null) {
				GameObject trigger = Instantiate (dropTrigger, this.transform);
				trigger.transform.parent = null;
				trigger.GetComponent< DropTrigger > ().messege = messege;
			}
			Destroy (this.gameObject);
		}
	}
}
