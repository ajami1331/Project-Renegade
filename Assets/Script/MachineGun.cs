using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
/// <summary>
/// MachineGun. 
/// </summary>
public class MachineGun : MonoBehaviour {

	public GameObject cameraObject;

	public Animator anim;

	public float roundsPerMinute;
	public float fireRate;
	public float fireTimer;

	public int roundsPerMag;
	public int totalRounds;
	public int roundsMag;

	public bool isReloading;
	public float reloadTime;
	public float reloadTimer;

	public float force = 50;

	public AudioClip fireSound;
	public AudioClip reloadSound;

	AudioSource fireSoundSource;
	AudioSource relaodSoundSource;

	public bool hasFlash;
	public GameObject muzzleFlash;
	public ParticleSystem muzzleFlashParticle;

	public GameObject playerManagerObj;
	public PlayerManager playerManager;

	public int damage = 25;

	public GameObject shootPosition;

	private AudioSource AddAudio( AudioClip clip, bool loop, bool playOnAwake, float volume )
	{
		AudioSource ret = gameObject.AddComponent< AudioSource >();
		ret.clip = clip;
		ret.loop = loop;
		ret.playOnAwake = playOnAwake;
		ret.volume = volume;
		return ret;
	}

	private void Awake()
	{
		cameraObject = GameObject.FindWithTag("PivotCamera");
		fireSoundSource = AddAudio( fireSound, false, false, 0.5f ); 
	}

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
		fireRate = 1.0f / ( roundsPerMinute / 60.0f );
		fireTimer = 0.0f;
		roundsMag = Mathf.Min(totalRounds, roundsPerMag);
		totalRounds -= roundsMag;
		if (hasFlash) {
			muzzleFlashParticle = muzzleFlash.GetComponent< ParticleSystem > ();
			muzzleFlash.SetActive (false);
		}
		playerManagerObj = GameObject.FindGameObjectWithTag ( "Manager" );
		playerManager = playerManagerObj.GetComponent < PlayerManager > ();
	}

	private void FixedUpdate()
	{

	}

	void Fire()
	{
		if( fireTimer < fireRate || roundsMag == 0 )
		{
			return;
		}

		anim.SetTrigger("Fire");
		fireSoundSource.Play();

		if (hasFlash) {
			muzzleFlash.SetActive (true);
		}

		playerManager.shotCount++;

		RaycastHit hit;

		if( Physics.Raycast( shootPosition.transform.position, cameraObject.transform.forward, out hit, 1000.0f ) )
		{
			//Debug.Log(hit.transform.name);
			//Debug.Log(hit.transform.tag);
			if( hit.rigidbody )
			{
				hit.rigidbody.AddForceAtPosition(force * cameraObject.transform.forward, hit.point);
			}
			if (hit.transform.tag == "Damageable" || hit.transform.tag == "Enemy" ) {
				hit.transform.gameObject.BroadcastMessage ( "ApplyDamage", damage );
				if (hit.transform.tag == "Enemy") {
					hit.transform.gameObject.BroadcastMessage ("gotHit");
					playerManager.comboTimer = 0f;
				}
				playerManager.hitCount++;
			}
		}

		fireTimer = 0.0f;
		//roundsMag--;
	}

	void UpdateRounds()
	{
		int prevRounds = roundsMag;
		roundsMag = Mathf.Min(roundsPerMag, totalRounds);
		totalRounds -= Mathf.Abs(roundsMag - prevRounds);
	}

	void Reload()
	{
		if( roundsMag == roundsPerMag || totalRounds == 0 )
		{
			return;
		}
		isReloading = true;
		reloadTimer = 0.0f;
		//anim.SetTrigger("Reload");
	}

	// Update is called once per frame
	void Update()
	{
		if(CrossPlatformInputManager.GetButton( "Reload" ) && !isReloading )
		{
			Reload();
		}
		if(CrossPlatformInputManager.GetButton( "Fire1" ) && !isReloading && roundsMag > 0 )  
		{
			Fire();
		}
		else if(isReloading)
		{
			if (reloadTimer < reloadTime)
			{
				reloadTimer += Time.deltaTime;
			}
			else
			{
				reloadTimer = 0.0f;
				isReloading = false;
				UpdateRounds();
			}
		}
		else if (roundsMag == 0)
		{
			Reload();
		}
		if( fireTimer < fireRate )
		{
			fireTimer += Time.deltaTime;
		}
		else {
			if (hasFlash) {
				muzzleFlash.SetActive (false);
			}	
		}
	}

	private void LateUpdate()
	{

	}
}
