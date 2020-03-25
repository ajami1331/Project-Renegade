using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy ( gameObject, 12f );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
