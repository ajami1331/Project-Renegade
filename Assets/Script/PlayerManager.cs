using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    
	// Use this for initialization
    GameObject cameraObject;
    GameObject player;
    GameObject activeWeapon;
    Text ammoDisplay;
    PlayerInfo playerInfo;
	public int grenadeCount;
	public int grenadeLimit = 3;

	public GameObject touchControl;
	public GameObject gamePanel;
	public GameObject gameMenu;

	public GameObject fireButton;
	public GameObject reloadButton;
	public Image fireButtonImage;

	public Sprite fireButtonImageGun;
	public Sprite fireButtonImageMelee;

	public GameObject crossHair;

	public float audioVolume;
	public float mouseSensitivty;

	public int levelKillCount;
	public int totalKillCount;
	public int levelScore;
	public int totalScore;
	public int hitCount;
	public int shotCount;

	public float comboTime = 2f;
	public float comboTimer = 0f;

	public int comboPoint = 5;
	public int comboX = 0;

	public Text comboDisplay;
	public Text grenadeDisplay;

	public float grenadeThrowForce = 1200f;

	public float pickupRange = 3f;

	public GameObject levelOver;
	public GameObject gameOver;
	public GameObject deathStatsTextObj;
	public Text deathStatsText;
	public GameObject levelOverStatsTextObj;
	public Text levelOverStatsText;

	public int totalHitCount;
	public int totalShotCount;

	public bool machineGunPick = false;

	public GameObject functionButton;
	public bool functionButtonOn;

	public float interval = 0.1f;
	public float usedInterval;
	public float intervalTimer = 0f;


    private void Awake() {
		functionButton = GameObject.FindGameObjectWithTag ( "Function" );
		AudioListener.pause = false;
        cameraObject = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
        activeWeapon = GameObject.FindWithTag("ActiveWeapon");
		ammoDisplay = GameObject.FindWithTag("AmmoDisplay").GetComponent<Text>();
		grenadeDisplay = GameObject.FindWithTag("GrenadeDisplay").GetComponent<Text>();
		fireButton = GameObject.FindWithTag("FireButton");
		reloadButton = GameObject.FindWithTag("ReloadButton");
		crossHair = GameObject.FindWithTag("CrossHair");
		comboDisplay = GameObject.FindWithTag("ComboDisplay").GetComponent<Text>();
		deathStatsText = deathStatsTextObj.GetComponent< Text > ( );
		levelOverStatsText = levelOverStatsTextObj.GetComponent< Text > ( );
		changeIcon ();

		if (PlayerPrefs.HasKey ("AudioVolume")) {
			audioVolume = PlayerPrefs.GetFloat ("AudioVolume");
		} 
		else {
			audioVolume = 1.0f;
			PlayerPrefs.SetFloat ("AudioVolume", audioVolume);
		}
		if (PlayerPrefs.HasKey ("MouseSensitivty")) {
			mouseSensitivty = PlayerPrefs.GetFloat ("MouseSensitivty");
		} else {
			mouseSensitivty = 2.5f;
			PlayerPrefs.SetFloat ("MouseSensitivty", mouseSensitivty );
		}


		levelScore = 0;
		levelKillCount = 0;

		if (PlayerPrefs.HasKey ("TotalKillCount")) {
			totalKillCount = PlayerPrefs.GetInt ("TotalKillCount");
		} else {
			totalKillCount = 0;
			PlayerPrefs.SetInt ("TotalKillCount", totalKillCount);
		}

		if (PlayerPrefs.HasKey ("TotalScore")) {
			totalScore = PlayerPrefs.GetInt ("TotalScore");
		} else {
			totalScore = 0;
			PlayerPrefs.SetInt ("TotalScore", totalScore);
		}


		totalHitCount = PlayerPrefs.GetInt ("HitCount");
		totalShotCount = PlayerPrefs.GetInt ("ShotCount");

		setVolume ( audioVolume );
		setSensitivity ( mouseSensitivty );


		comboPoint = 5;
		comboX = 0;

		comboDisplay.text = System.String.Empty;

		comboTime = 15f;

		interval = 0.025f;

		functionButtonOn = true;

		Time.timeScale = 0;
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Time.frameCount % 30 == 0) {
			System.GC.Collect();
		}

		usedInterval = interval;
		if(Time.deltaTime > usedInterval) usedInterval = Time.deltaTime;

		if (intervalTimer > usedInterval) {
			intervalTimer = 0f;
			RaycastHit hit;
        
			if (Physics.Raycast (cameraObject.transform.position, cameraObject.transform.forward, out hit, pickupRange)) {
				//Debug.Log(hit.transform.name);
				if (hit.transform.tag == "MountableTurret" && machineGunPick == false) {
					UIElementOn (functionButton);
					functionButtonOn = true;
					if (CrossPlatformInputManager.GetButtonUp ("Function")) {
						hit.transform.BroadcastMessage ("Active");
						machineGunPick = true;
						fireButtonImage.sprite = fireButtonImageGun;
						UIElementOff (crossHair);
						player.SetActive (false);
						hit.transform.Find ("DummyTurret").gameObject.SetActive (false);
						hit.transform.Find ("Pivot").gameObject.SetActive (true);
						PlayerTurret turret = hit.transform.Find ("Pivot").gameObject.GetComponent< PlayerTurret > ();
						turret.lookSensitivity = mouseSensitivty;
						ammoDisplay.text = string.Format ("MachinGun: inf");
					}
				} else if (hit.transform.tag == "GunPickup") {
					UIElementOn (functionButton);
					functionButtonOn = true;
					if (CrossPlatformInputManager.GetButtonDown ("Function")) {
						GunObject gunObj = hit.transform.GetComponent<GunObject> ();
						Gun gun = activeWeapon.GetComponent<Gun> ();
						if (gunObj.gunName != gun.gunName) {
							GameObject gObj1 = Instantiate (gunObj.gunAvatar, activeWeapon.transform.parent.position, cameraObject.transform.rotation);
							GameObject gObj2 = Instantiate (gun.pickupPrefab, hit.transform.position + new Vector3 (0.0f, 0.5f, 0.0f), hit.transform.rotation);
							gObj1.transform.parent = activeWeapon.transform.parent;
							gObj2.GetComponent<GunObject> ().totalRounds = gun.totalRounds + gun.roundsMag;
							gObj1.SetActive (true);
							gObj2.SetActive (true);
							Destroy (activeWeapon);
							Destroy (hit.transform.gameObject);
							activeWeapon = gObj1;
							gun = activeWeapon.GetComponent<Gun> ();
							gun.totalRounds = gunObj.totalRounds;
							changeIcon ();
						} else {
							if (gun.totalRounds < gun.roundCarryLimit) {
								gun.totalRounds += gunObj.totalRounds;
								gun.totalRounds = Mathf.Min (gun.totalRounds, gunObj.totalRounds);
								Destroy (hit.transform.gameObject);
							}
						}
					}
				} else if (hit.transform.tag == "GrenadePickup") {
					UIElementOn (functionButton);
					functionButtonOn = true;
					//Debug.Log (hit.transform.tag);
					if (CrossPlatformInputManager.GetButtonDown ("Function")) {
						if (grenadeCount < grenadeLimit) {
							grenadeCount++;
							Destroy (hit.transform.gameObject);
						}
					}
				}
				//Debug.Log (hit.transform.tag);
			} else if( functionButtonOn ) {
				functionButtonOn = false;
				UIElementOff (functionButton);
			}
		}

		if (CrossPlatformInputManager.GetButton ("Escape") || Input.GetKeyDown( KeyCode.Escape ) ) {
			if (gamePanel.activeInHierarchy) {
				touchControl.SetActive (false);
				gamePanel.SetActive (false);
				gameMenu.SetActive (true);
				Pause ();
			}
		}

		if (comboTimer < comboTime) {
			comboTimer += Time.deltaTime;
		}
		else {
			comboPoint = 5;
			comboX = 0;
			comboDisplay.text = System.String.Empty;
		}

		intervalTimer += Time.deltaTime;
    }

    private void LateUpdate()
    {
        Gun gun = activeWeapon.GetComponent<Gun>();
		if (gun.isMelee) {
			ammoDisplay.text = string.Format ("{0} ", gun.gunName );	
		}
		else {
			ammoDisplay.text = string.Format ("{0}: {1}/{2}", gun.gunName, gun.roundsMag, gun.totalRounds);
		}
		grenadeDisplay.text = string.Format ("Grenades: {0} ", grenadeCount );
		machineGunPick = false;
    }

    public void Pause()
    {
		AudioListener.pause = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
		AudioListener.pause = false;
    }

	void changeIcon() {
		UIElementOn ( crossHair, 32f, 32f );
		fireButtonImage = fireButton.GetComponent < Image > ();
		if (activeWeapon.GetComponent < Gun > ().isMelee) {
			fireButtonImage.sprite = fireButtonImageMelee;
			UIElementOff ( reloadButton );
		}
		else {
			fireButtonImage.sprite = fireButtonImageGun;
			UIElementOn ( reloadButton );
		}
	}

	void UIElementOff( GameObject uiElement ) {
		RectTransform rect = uiElement.GetComponent < RectTransform > ();
		rect.sizeDelta = new Vector2 (0f, 0f);
	}

	void UIElementOn( GameObject uiElement, float sizeX = 96f, float sizeY = 96f ) {
		RectTransform rect = uiElement.GetComponent < RectTransform > ();
		rect.sizeDelta = new Vector2 (sizeX, sizeY);
	}

	public void setVolume( float volume ) {
		AudioListener.volume = volume;
		audioVolume = volume;
		PlayerPrefs.SetFloat ("AudioVolume", audioVolume);
		PlayerPrefs.Save ();
	}

	public void setSensitivity( float value ) {
		cameraObject.GetComponent< MouseLook >().SetLookSensitivity( value );
		mouseSensitivty = value;
		PlayerPrefs.SetFloat ("MouseSensitivty", mouseSensitivty );
		PlayerPrefs.Save ();
	}

	public void UpdateKill() {
		comboTimer = 0f;
		if (comboTimer < comboTime) {
			comboPoint += 5;
			comboX++;
			comboPoint = Mathf.Min ( 25, comboPoint );
			comboX = Mathf.Min (5, comboX);
			if (comboX > 1) {
				comboDisplay.text = string.Format ("{0}x combo ", comboX );	
			}
		} 
		else {
			comboPoint = 5;
		}
		levelKillCount++;
		levelScore += comboPoint;
		PlayerPrefs.SetInt ("TotalKillCount", totalKillCount + levelKillCount);
		PlayerPrefs.SetInt ("TotalScore", totalScore + levelScore);
	}

	public void ReloadScene() {
		Time.timeScale = 1;
		Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);
	}

	public void PlayerDead() {
		gameOver.SetActive ( true );
		Time.timeScale = 0;
		AudioListener.pause = true;
		//totalKillCount = PlayerPrefs.GetInt ("TotalKillCount");
		//totalScore = PlayerPrefs.GetInt ("TotalScore");
		//int totalHitCount = PlayerPrefs.GetInt ("HitCount");
		//int totalShotCount = PlayerPrefs.GetInt ("ShotCount");
		int divShot = Mathf.Max( shotCount, 1 );
		float accuracy = ( ((float)hitCount) / ((float)divShot) ) * 100f;
		deathStatsText.text = string.Format ( "Level Kill: {0}\nLevel Score: {1}\nLevel Hit: {2}\nLevel Shot: {3}\nLevel Accuracy: {4}%\n",
			levelKillCount, levelScore, hitCount, shotCount, accuracy );

		PlayerPrefs.SetInt ("TotalKillCount", totalKillCount + levelKillCount);
		PlayerPrefs.SetInt ("TotalScore", totalScore + levelScore);
		PlayerPrefs.SetInt ("HitCount", hitCount + totalHitCount);
		PlayerPrefs.SetInt ("ShotCount", shotCount + totalShotCount);
	}

	public void LevelOver() {
		Time.timeScale = 0;
		AudioListener.pause = true;
		//totalKillCount = PlayerPrefs.GetInt ("TotalKillCount");
		//totalScore = PlayerPrefs.GetInt ("TotalScore");
		//int totalHitCount = PlayerPrefs.GetInt ("HitCount");
		//int totalShotCount = PlayerPrefs.GetInt ("ShotCount");
		int divShot = Mathf.Max( shotCount, 1 );
		float accuracy = ( ((float)hitCount) / ((float)divShot) ) * 100f;
		levelOverStatsText.text = string.Format ( "Level Kill: {0}\nLevel Score: {1}\nLevel Hit: {2}\nLevel Shot: {3}\nLevel Accuracy: {4}%\n",
			levelKillCount, levelScore, hitCount, shotCount, accuracy );

		PlayerPrefs.SetInt ("TotalKillCount", totalKillCount + levelKillCount);
		PlayerPrefs.SetInt ("TotalScore", totalScore + levelScore);
		PlayerPrefs.SetInt ("HitCount", hitCount + totalHitCount);
		PlayerPrefs.SetInt ("ShotCount", shotCount + totalShotCount);
	}

	public void UpdateKills() {
		PlayerPrefs.SetInt ("TotalKillCount", totalKillCount + levelKillCount);
		PlayerPrefs.SetInt ("TotalScore", totalScore + levelScore);
		PlayerPrefs.SetInt ("HitCount", hitCount + totalHitCount);
		PlayerPrefs.SetInt ("ShotCount", shotCount + totalShotCount);
		PlayerPrefs.Save ();
	}

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

	public void QuitToMainMenu() {
		UpdateKills ();
		Time.timeScale = 1;
		SceneManager.LoadScene( 0+2, LoadSceneMode.Single);
	}

	void OnApplicationFocus(bool hasFocus) {
		if (!hasFocus) {
			if (gamePanel.activeInHierarchy) {
				touchControl.SetActive (false);
				gamePanel.SetActive (false);
				gameMenu.SetActive (true);
				Pause ();
			}
		}
		//else {
			//if (!gamePanel.activeInHierarchy) {
			//	touchControl.SetActive (true);
			//	gamePanel.SetActive (true);
			//	gameMenu.SetActive (false);
			//	UnPause ();
			//}
		//}
	}


	public void Init() {
		Gun gun = activeWeapon.GetComponent<Gun>();
		if (gun.isMelee) {
			ammoDisplay.text = string.Format ("{0} ", gun.gunName );	
		}
		else {
			ammoDisplay.text = string.Format ("{0}: {1}/{2}", gun.gunName, gun.roundsMag, gun.totalRounds);
		}
		grenadeDisplay.text = string.Format ("Grenades: {0} ", grenadeCount );
		machineGunPick = false;
	}
}


