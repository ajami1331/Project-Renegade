using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

	// Use this for initialization
	public float timeTileBoom = 5f;
	public GameObject explosion;
	public GameObject insBoom;
	public int damage = 200;
	public float radius = 10f;
	public float force = 120f;

	public IEnumerator Boom() {
		yield return new WaitForSeconds( timeTileBoom );
		insBoom = Instantiate (explosion, this.transform);
		insBoom.transform.position = this.transform.position;
		insBoom.transform.parent = null;

		Collider[] colliders = Physics.OverlapSphere (this.transform.position, radius);

		foreach (Collider hit in colliders) {
			if (hit.transform.tag == "Damageable" || hit.transform.tag == "Player" || hit.transform.tag == "Enemy" ) {
				hit.transform.gameObject.BroadcastMessage ( "ApplyDamage", damage );
			}
			Rigidbody rb = hit.GetComponent < Rigidbody > ();
			if (rb != null) {
				rb.AddExplosionForce ( force, this.transform.position, radius, 3.0f );
			}
		}

		Destroy (this.gameObject);
	}

	void Start () {
		StartCoroutine (Boom ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
