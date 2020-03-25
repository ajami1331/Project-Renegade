using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwo : MonoBehaviour {

	public GameObject[] areas;
	public GameObject[] triggers;
	public GameObject GameFinish;
	public GameObject touchControl;
	public GameObject levelUI;

	void ShowArea1()
	{
		areas [0].SetActive (true);
		triggers[0].SetActive(false);
		triggers[1].SetActive(true);
	}

	void ShowArea2()
	{
		areas [1].SetActive (true);
		triggers[1].SetActive(false);
		triggers[2].SetActive(true);
	}

	void ShowArea3()
	{
		areas [2].SetActive (true);
		triggers[2].SetActive(false);
		triggers[3].SetActive(true);
	}

	void ShowArea4()
	{
		areas [3].SetActive (true);
		triggers[3].SetActive(false);
		triggers[4].SetActive(true);
	}

	void ShowArea5()
	{
		areas [4].SetActive (true);
		triggers[4].SetActive(false);
		triggers[5].SetActive(true);
	}

	void ShowArea6()
	{
		areas [5].SetActive (true);
		triggers[5].SetActive(false);
		triggers[6].SetActive(true);
	}

	void LevelOverTwo()
	{
		AudioListener.pause = true;
		GameObject.FindGameObjectWithTag ( "Manager" ).GetComponent< PlayerManager >().UpdateKills();
		GameFinish.SetActive (true);
		touchControl.SetActive (false);
		levelUI.SetActive (false);
	}

	public void ReloadScene() {
		Time.timeScale = 1;
		SceneManager.LoadScene( 3+2, LoadSceneMode.Single);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
