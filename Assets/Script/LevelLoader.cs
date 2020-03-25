using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	private bool loadScene = false;

	[SerializeField]
	private int scene;
	[SerializeField]
	private Text loadingText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (loadScene == true) {
			loadingText.color = new Color (loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong (Time.time, 1));
		} 
		else {
			loadScene = true;
			StartCoroutine ( LoadNewScene() );
		}
	}

	IEnumerator LoadNewScene() {

		AsyncOperation async = SceneManager.LoadSceneAsync (scene, LoadSceneMode.Single);

		while (!async.isDone) {
			yield return null;
		}

	}
}
