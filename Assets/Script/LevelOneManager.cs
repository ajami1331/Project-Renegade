using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneManager : MonoBehaviour {

	public GameObject[] areas;
	public GameObject[] triggers;
	public GameObject levelOver;
	public GameObject playerManager;
	public GameObject[] texts;
	public GameObject touchControl;

	void SpawnArea1()
	{
		areas [0].SetActive (true);
	}

	void SpawnArea2()
	{
		areas [1].SetActive (true);
	}

	void SpawnArea3()
	{
		areas [2].SetActive (true);
	}

	void SpawnArea4()
	{
		areas [3].SetActive (true);
	}

	void SpawnArea5()
	{
		areas [4].SetActive (true);
	}

	void SpawnArea6()
	{
		areas [5].SetActive (true);
		texts [0].SetActive (true);
	}

	void SpawnArea7()
	{
		areas [6].SetActive (true);
	}

	void SpawnArea8()
	{
		areas [7].SetActive (true);
	}
		

	public void MainMenu() {
		Time.timeScale = 1;
		SceneManager.LoadScene( 0+2, LoadSceneMode.Single);
	}

	public void NextLevel() {
		Time.timeScale = 1;
		SceneManager.LoadScene( 3+2, LoadSceneMode.Single);
	}

	public void ReloadScene() {
		Time.timeScale = 1;
		SceneManager.LoadScene( 2+2, LoadSceneMode.Single);
	}

	void GameOver() {
		playerManager.GetComponent< PlayerManager > ().LevelOver();
		playerManager.GetComponent< PlayerManager > ().UpdateKills();
		Time.timeScale = 0;
		levelOver.SetActive (true);
		touchControl.SetActive (false);
		int levelUnlocked = PlayerPrefs.GetInt ("LevelUnlocked");
		levelUnlocked = Mathf.Max( 2, levelUnlocked );
		PlayerPrefs.SetInt ("LevelUnlocked", levelUnlocked);
		PlayerPrefs.Save ();
	}

	// Use this for initialization
	void Start () {
		levelOver.SetActive (false);
		playerManager = GameObject.FindWithTag("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
