using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int health = 100;
	public GameObject ragdoll;
	public GameObject body;
	public GameObject weaponDrop;
	public GameObject weapon;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void ApplyDamage( int h ) {
		health -= h;
		if (health <= 0) {
			body = Instantiate (ragdoll, this.transform);
			body.transform.parent = null;
			weapon = Instantiate (weaponDrop, this.transform);
			weapon.transform.parent = null;
			body.transform.position = this.transform.position + new Vector3 ( 0f, 0f, 0f );
			body.transform.rotation = this.transform.rotation;
			weapon.transform.position = this.transform.position + new Vector3 ( 0f, 1f, 0f );
			weapon.GetComponent < Rigidbody > ().AddForce ( this.transform.forward * 50f );
			GameObject.FindGameObjectWithTag ("Manager").GetComponent< PlayerManager >().UpdateKill ();
			Destroy (this.gameObject);
		}
	}
}
