using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
/// <summary>
/// Gun. 
/// </summary>
public class Gun : MonoBehaviour {

    public GameObject cameraObject;

    public Animator anim;

    public string gunName;

	public bool isMelee = false;

	public float range = 1000f;
    public float roundsPerMinute;
    public float fireRate;
    public float fireTimer;

	public int roundCarryLimit = 320;
    public int roundsPerMag;
    public int totalRounds;
    public int roundsMag;

    public bool isReloading;
	public bool isThrowing;
    public float reloadTime;
    public float reloadTimer;


    public float force = 50f;

    public GameObject pickupPrefab;

	public AudioClip fireSound;
	public AudioClip reloadSound;

	public float reloadVolume = 1.0f;
	public float fireVolume = 0.5f;

	AudioSource fireSoundSource;
	AudioSource reloadSoundSource;

	public PlayerManager playerManager;

	public GameObject GrenadeObj;

	public float throwForce = 500f;

	public bool hasFlash;
	public GameObject muzzleFlash;
	public ParticleSystem muzzleFlashParticle;

	public int damage = 25;


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
        cameraObject = GameObject.FindWithTag("MainCamera");
		fireSoundSource = AddAudio( fireSound, false, false, fireVolume ); 
		reloadSoundSource = AddAudio (reloadSound, false, false, reloadVolume);
    }

    // Use this for initialization
    void Start ()
    {
		playerManager = GameObject.FindWithTag("Manager").GetComponent<PlayerManager>();
        anim = GetComponent<Animator>();
        fireRate = 1.0f / ( roundsPerMinute / 60.0f );
        fireTimer = 0.0f;
        roundsMag = Mathf.Min(totalRounds, roundsPerMag);
        totalRounds -= roundsMag;
		if (hasFlash) {
			muzzleFlashParticle = muzzleFlash.GetComponent< ParticleSystem > ();
			muzzleFlash.SetActive (false);
		}
		throwForce = playerManager.grenadeThrowForce;
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

        RaycastHit hit;
		if( !isMelee ) playerManager.shotCount++;

        if( Physics.Raycast( cameraObject.transform.position, cameraObject.transform.forward, out hit, range ) )
        {
            //Debug.Log(hit.transform.name);
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
				if( !isMelee ) playerManager.hitCount++;
			}
        }
        fireTimer = 0.0f;
		if( !isMelee )roundsMag--;
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
        anim.SetTrigger("Reload");
		reloadSoundSource.Play ();
    }

	public IEnumerator ThrowGrenade() {
		yield return new WaitForSeconds( 3.0f );
		GameObject nade = Instantiate( GrenadeObj, cameraObject.transform.position + new Vector3( 0.7f, 0f, 0f ), cameraObject.transform.rotation );
		nade.GetComponent< Rigidbody > ().AddForce( cameraObject.transform.forward * throwForce );
		yield return new WaitForSeconds( 1.0f );
		isThrowing = false;
	}

	void Grenade() {
		playerManager.grenadeCount--;
		anim.SetTrigger ("Grenade");
		StartCoroutine( ThrowGrenade() );
	}

    // Update is called once per frame
    void Update()
    {
		if (CrossPlatformInputManager.GetButton ("Reload") && !isReloading && !isThrowing ) {
			Reload ();
		} else if (CrossPlatformInputManager.GetButton ("Fire1") && !isReloading && !isThrowing && roundsMag > 0) {
			Fire ();
		} else if (isReloading) {
			if (reloadTimer < reloadTime) {
				reloadTimer += Time.deltaTime;
			} else {
				reloadTimer = 0.0f;
				isReloading = false;
				UpdateRounds ();
			}
		} else if (roundsMag == 0) {
			Reload ();
		} else if (CrossPlatformInputManager.GetButton ("Grenade") && !isReloading && !isThrowing && playerManager.grenadeCount > 0) {
			isThrowing = true;
			Grenade();
		}
		if (fireTimer < fireRate) {
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
