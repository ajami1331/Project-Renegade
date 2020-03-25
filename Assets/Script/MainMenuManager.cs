using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization

	public GameObject MainPanel;
	public GameObject QuitPanel;
	public int levelUnlocked;
	public GameObject statsTextObject;
	public Text statsText;
	public int totalKillCount;
	public int totalScore;
	public int hitCount;
	public int shotCount;

	public void Quit() {
	#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
	#else
		Application.Quit();
	#endif
	}

	public void LoadLevelZero () {
		if (levelUnlocked >= 0) { 
			SceneManager.LoadScene (1+2, LoadSceneMode.Single);
		}
	}

	public void LoadLevelOne () {
		if (levelUnlocked >= 1) { 
			SceneManager.LoadScene (2+2, LoadSceneMode.Single);
		}
	}

	public void LoadLevelTwo () {
		if (levelUnlocked >= 2) { 
			SceneManager.LoadScene (3+2, LoadSceneMode.Single);
		}
	}

	public void setVolume( float volume ) {
		AudioListener.volume = volume;
		float audioVolume = volume;
		PlayerPrefs.SetFloat ("AudioVolume", audioVolume);
		PlayerPrefs.Save ();
	}

	public void setSensitivity( float value ) {
		float mouseSensitivty = value;
		PlayerPrefs.SetFloat ("MouseSensitivty", mouseSensitivty );
		PlayerPrefs.Save ();
	}

	public void SetGodMode( float value ) {
		bool godMode = value > 0f ? true : false;
		int godModeInt = godMode ? 1 : 0;
		PlayerPrefs.SetInt ("GodMode", godModeInt);
		PlayerPrefs.Save ();
	}

	void Start () {
		AudioListener.pause = false;
		levelUnlocked = PlayerPrefs.GetInt ("LevelUnlocked");
		statsText = statsTextObject.GetComponent< Text >();
		totalKillCount = PlayerPrefs.GetInt ("TotalKillCount");
		totalScore = PlayerPrefs.GetInt ("TotalScore");
		hitCount = PlayerPrefs.GetInt ("HitCount");
		shotCount = PlayerPrefs.GetInt ("ShotCount");
		int divShot = Mathf.Max( shotCount, 1 );
		float accuracy = ( ((float)hitCount) / ((float)divShot) ) * 100f;
		statsText.text = string.Format ( "Total Kill: {0}\nTotal Score: {1}\nTotal Hit: {2}\nTotal Shot: {3}\nAccuracy: {4}%\n",
			totalKillCount, totalScore, hitCount, shotCount, accuracy );
	}

	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown ("Escape")) {
			MainPanel.SetActive ( false );
			QuitPanel.SetActive ( true );
		}
	}

	public void ResetStats() {
		PlayerPrefs.SetInt ("TotalKillCount", 0);
		PlayerPrefs.SetInt ("TotalScore", 0);
		PlayerPrefs.SetInt ("HitCount", 0);
		PlayerPrefs.SetInt ("ShotCount", 0);
		PlayerPrefs.Save();
		float accuracy = 0.00f;

		statsText.text = string.Format ( "Total Kill: {0}\nTotal Score: {1}\nTotal Hit: {2}\nTotal Shot: {3}\nAccuracy: {4}%\n",
			0, 0, 0, 0, accuracy );
	}	
}

