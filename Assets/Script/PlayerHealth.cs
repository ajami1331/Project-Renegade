using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public bool godMode = false;

	public GameObject cameraObj;
	public GameObject playerManager;
	public VignetteAndChromaticAberration effect;
	public int maxHealth = 100;
	public int health;
	public float percent;
	public AudioClip heartbeatSound;
	public AudioSource heartbeatSoundSource;
	public float heartbeatVolume = 1f;

	public bool recoverTimerFlag = false;
	public bool recovering = false;
	public float recoverTime = 3f;
	public float recoverTimer;
	public int recoverHealthAmount;
	public float recoverHealthTime = 1f;
	public float recoverHealthTimer = 0f;

	public GameObject touchControl;
	public GameObject gameOver;
	public GameObject gamePanel;

	private AudioSource AddAudio( AudioClip clip, bool loop, bool playOnAwake, float volume ) {
		AudioSource ret = gameObject.AddComponent< AudioSource >();
		ret.clip = clip;
		ret.loop = loop;
		ret.playOnAwake = playOnAwake;
		ret.volume = volume;
		return ret;
	}

	// Use this for initialization
	void Start () {
		touchControl = GameObject.FindGameObjectWithTag ("TouchControl");
		cameraObj = GameObject.FindGameObjectWithTag ("MainCamera");
		playerManager = GameObject.FindGameObjectWithTag ("Manager");
		health = maxHealth;
		effect = cameraObj.GetComponent < VignetteAndChromaticAberration > ();
		heartbeatSoundSource = AddAudio( heartbeatSound, true, false, heartbeatVolume );
		if (PlayerPrefs.HasKey ("GodMode")) {
			godMode = PlayerPrefs.GetInt ("GodMode") == 1 ? true : false;
		} 
		else {
			godMode = false;
			int godModeInt = 0;
			PlayerPrefs.SetInt ("GodMode", (int)godModeInt);
		}
		PlayerPrefs.Save ();
	}

	public void SetGodMode( float value ) {
		godMode = value > 0f ? true : false;
		int godModeInt = godMode ? 1 : 0;
		PlayerPrefs.SetInt ("GodMode", godModeInt);
		PlayerPrefs.Save ();
	}
	
	// Update is called once per frame
	void Update () {
		if ( recoverTimerFlag && recoverTimer < recoverTime) {
			recoverTimer += Time.deltaTime;
		} 
		else if (!recovering && health < maxHealth) {
			recovering = true;
			recoverHealthTimer = 0f;
			recoverTimer = 0f;
			recoverTimerFlag = false;
		}

		if (recovering) {
			if (recoverHealthTimer < recoverHealthTime) {
				recoverHealthTimer += Time.deltaTime;
			} else {
				health += recoverHealthAmount;
				recoverHealthAmount += 5;
				if (health >= maxHealth) {
					health = maxHealth;
					recovering = false;
					recoverHealthAmount = 5;
				}
				recoverHealthTimer = 0f;
			}
		}

		percent = Mathf.Min( 60, (maxHealth - health) ) / 100f;
		effect.intensity = percent;
		effect.blur = percent;
		if (health <= 60 && !heartbeatSoundSource.isPlaying ) {
			heartbeatSoundSource.Play ();
		}
		if (health >= 80 && heartbeatSoundSource.isPlaying ) {
			heartbeatSoundSource.Stop ();
		}
	}

	void ApplyDamage( int h ) {
		if (godMode) {
			return;
		}
		health -= h;
		recoverTimer = 0f;
		recovering = false;
		recoverHealthAmount = 5;
		recoverTimerFlag = true;
		if (health <= 0) {
			touchControl.SetActive ( false );
			gamePanel.SetActive ( false );
			gameOver.SetActive ( true );
			Time.timeScale = 0;
			AudioListener.pause = true;
			playerManager.GetComponent < PlayerManager > ().PlayerDead ();
		}
	}
}
