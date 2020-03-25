using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelZero : MonoBehaviour {

	public GameObject[] texts;
	public GameObject[] arrows;
	public GameObject[] uiElements;
	public GameObject[] Triggers;
	public GameObject levelOver;
	public GameObject playerManager;

	public IEnumerator init() {
		texts [0].SetActive (true);
		yield return new WaitForSeconds (1.5f);
		texts [0].SetActive (false);
		texts [1].SetActive (true);
		yield return new WaitForSeconds (1.5f);
		texts [1].SetActive (false);
		texts [2].SetActive (true);
		yield return new WaitForSeconds (2.5f);
		texts [2].SetActive (false);
		texts [3].SetActive (true);
		arrows [0].SetActive (true);
		UIElementOn (0);
		yield return new WaitForSeconds (1.5f);
		texts [3].SetActive (false);
		arrows [1].SetActive (true);
		yield return new WaitForSeconds (1.5f);
		arrows [2].SetActive (true);
		yield return new WaitForSeconds (1.5f);
		arrows [3].SetActive (true);
	}
		
	void UIElementOff( int index ) {
		RectTransform rect = uiElements [index].GetComponent < RectTransform > ();
		rect.sizeDelta = new Vector2 (0f, 0f);
	}

	void UIElementOn( int index ) {
		RectTransform rect = uiElements [index].GetComponent < RectTransform > ();
		rect.sizeDelta = new Vector2 (96f, 96f);
	}

	void EnterRange() {
		texts [4].SetActive ( true );
		Triggers [0].SetActive (false);
	}

	void RangeWelcome() {
		texts [5].SetActive ( true );
		//UIElementOn (1);
		//UIElementOn (2);
		UIElementOn (3);
		//UIElementOn (4);
		Triggers [1].SetActive (false);
	}

	void GunRange() {
		texts [5].SetActive ( false );
		texts [6].SetActive ( true );
		texts [10].SetActive ( true );
		texts [14].SetActive ( true );
		arrows [4].SetActive (true);
		arrows [6].SetActive (true);
		arrows [8].SetActive (true);
		Triggers [2].SetActive (false);
	}

	void ShootPickUp() {
		texts [8].SetActive ( true );
		texts [12].SetActive ( true );
		texts [16].SetActive ( true );
		Triggers [3].SetActive (false);
	}

	void ChangeRoom() {
		texts [8].SetActive ( false );
		texts [12].SetActive ( false );
		texts [16].SetActive ( false );
		texts [17].SetActive ( true );
		arrows [10].SetActive ( true );
		Triggers [4].SetActive (false);
	}

	void MachineGunRoom() {
		texts [18].SetActive ( true );
		texts [19].SetActive ( true );
		arrows [11].SetActive ( true );
		Triggers [5].SetActive (false);

		arrows [0].SetActive ( false );
		arrows [1].SetActive ( false );
		arrows [2].SetActive ( false );
		arrows [3].SetActive ( false );
		arrows [4].SetActive ( false );
		arrows [5].SetActive ( false );
		arrows [6].SetActive ( false );
		arrows [7].SetActive ( false );
		arrows [8].SetActive ( false );
		arrows [9].SetActive ( false );
		arrows [10].SetActive ( false );

		texts [0].SetActive ( false );
		texts [1].SetActive ( false );
		texts [2].SetActive ( false );
		texts [3].SetActive ( false );
		texts [4].SetActive ( false );
		texts [5].SetActive ( false );
		texts [6].SetActive ( false );
		texts [7].SetActive ( false );
		texts [8].SetActive ( false );
		texts [9].SetActive ( false );
		texts [10].SetActive (false);
		texts [11].SetActive ( false );
		texts [12].SetActive ( false );
		texts [13].SetActive ( false );
		texts [14].SetActive ( false );
		texts [15].SetActive ( false );
		texts [16].SetActive ( false );
		texts [17].SetActive ( false );
	}

	void VIckersPickup() {
		texts [18].SetActive ( false );
		arrows [11].SetActive ( false );

		texts [20].SetActive ( true );
	}

	void GrenadeFollow() {
		for (int i = 0; i < 7; i++) {
			Triggers [i].SetActive (false);
		}
		for (int i = 0; i < 20; i++) {
			texts [i].SetActive (false);
		}
		for (int i = 0; i < 12; i++) {
			arrows [i].SetActive (false);
		}

		texts [19].SetActive ( false );
		texts [20].SetActive ( false );

		arrows [12].SetActive (true);
		arrows [13].SetActive (true);

		texts [21].SetActive (true);
	}

	void GrenadePickup() {
		arrows [14].SetActive (true);

		texts [22].SetActive (true);
	}

	void GrenadeThrow() {
		UIElementOn (4);

		arrows [14].SetActive ( false );
		texts [22].SetActive ( false );

		texts [23].SetActive ( true );

	}

	IEnumerator SandBagCoroutine() {
		texts [24].SetActive ( true );
		yield return new WaitForSeconds (2.5f);
		texts [24].SetActive ( false );
	}

	void SandBag() {
		texts [23].SetActive ( false );
		StartCoroutine ( SandBagCoroutine() );
	}

	IEnumerator ReportInstructorCoroutine() {
		texts [25].SetActive ( true );
		yield return new WaitForSeconds (2.5f);
		texts [25].SetActive ( false );
		texts [26].SetActive ( true );
		yield return new WaitForSeconds (2.5f);
		texts [26].SetActive ( false );

		for (int i = 0; i < 16; i++) {
			arrows [i].SetActive (false);
		}

		for (int i = 0; i < 27; i++) {
			texts [i].SetActive (false);
		}

		for (int i = 0; i < 11; i++) {
			Triggers [i].SetActive (false);
		}

		arrows [16].SetActive (true);
		arrows [17].SetActive (true);
		arrows [18].SetActive (true);
		arrows [19].SetActive (true);
		arrows [20].SetActive (true);
		arrows [21].SetActive (true);
		arrows [22].SetActive (true);
		arrows [23].SetActive (true);

		Triggers [11].SetActive (true);
	}

	void ReportInstructor() {
		StartCoroutine ( ReportInstructorCoroutine() );
	}

	IEnumerator PassedTrainingCoroutine() {
		yield return new WaitForSeconds (2.5f);
		GameOver ();
	}

	void PassedTraining() {
		texts [28].SetActive ( true );
		StartCoroutine ( PassedTrainingCoroutine() );
	}

	void GameOver() {
		UIElementOff (0);
		UIElementOff (1);
		UIElementOff (2);
		UIElementOff (3);
		UIElementOff (4);
		texts [28].SetActive (false);
		levelOver.SetActive (true);
		playerManager.GetComponent< PlayerManager > ().LevelOver();
		playerManager.GetComponent< PlayerManager > ().UpdateKills();
		Time.timeScale = 0;
		int levelUnlocked = PlayerPrefs.GetInt ("LevelUnlocked");
		levelUnlocked = Mathf.Max( 1, levelUnlocked );
		PlayerPrefs.SetInt ("LevelUnlocked", levelUnlocked);
		PlayerPrefs.Save ();
	}

	public void MainMenu() {
		Time.timeScale = 1;
		SceneManager.LoadScene( 0+2, LoadSceneMode.Single);
	}

	public void NextLevel() {
		Time.timeScale = 1;
		SceneManager.LoadScene( 2+2, LoadSceneMode.Single);
	}

	public void ReloadScene() {
		Time.timeScale = 1;
		SceneManager.LoadScene( 1+2, LoadSceneMode.Single);
	}

	// Use this for initialization
	void Start () {
		Triggers [11].SetActive (false);
		UIElementOff (0);
		UIElementOff (1);
		UIElementOff (2);
		UIElementOff (3);
		UIElementOff (4);
		levelOver.SetActive (false);
		playerManager = GameObject.FindWithTag("Manager");
		StartCoroutine ( init() );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
