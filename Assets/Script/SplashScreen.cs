using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	public float waitSeconds = 1.5f;
	public int scene;

	IEnumerator LoadScene() {
		yield return new WaitForSeconds( waitSeconds );
		SceneManager.LoadScene (scene, LoadSceneMode.Single);
	}

	void Start () {
		StartCoroutine ( LoadScene() );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
