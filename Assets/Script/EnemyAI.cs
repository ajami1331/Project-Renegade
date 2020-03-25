using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public Animator anim;
	public GameObject player;
	public Transform[] waypoints;
	public int currentState = 0;
	public int waypointIndex = 0;
	public int waypointSize;
	public float reachDistance = 0.1f;
	public float speed = 1.5f;
	public float gravity = 20.0f;
	public float rotationSpeed = 3f;
	public int damage = 15;
	public float range = 20f;
	public float viewAngle = 60f;

	public float roundsPerMinute = 660f;
	public float fireRate;
	public float fireTimer;

	public float fireVolume = 1f;
	public AudioClip fireSound;
	AudioSource fireSoundSource;


	public float hitTime = 2.4f;
	public float hitTimer;

	enum states {
		Idle,
		Patrol,
		Shoot,
		Hit
	};

	private AudioSource AddAudio( AudioClip clip, bool loop, bool playOnAwake, float volume ) {
		AudioSource ret = gameObject.AddComponent< AudioSource >();
		ret.clip = clip;
		ret.loop = loop;
		ret.playOnAwake = playOnAwake;
		ret.volume = volume;
		return ret;
	}

	void Awake() {
		fireSoundSource = AddAudio( fireSound, false, false, fireVolume ); 
	}

	void Start () {
		hitTimer = 2.5f;
		anim = GetComponent< Animator > ();
		currentState = (int)states.Idle;
		player = GameObject.FindWithTag("Player");
		fireRate = 1.0f / ( roundsPerMinute / 60.0f );
		fireTimer = 0.0f;
	}

	bool checkAngle() {
		Vector3 target = player.transform.position;
		target.y = this.transform.position.y;
		Vector3 moveDirection = target - this.transform.position; 
		if( Mathf.Abs( Vector3.Angle( this.transform.forward, moveDirection ) ) <= viewAngle ) return true;
		return false;
	}

	bool canSee() {
		float distance = Vector3.Distance (player.transform.position, this.transform.position);
		if (distance > range) {
			return false;
		} else {
			RaycastHit hit;
			Vector3 target = player.transform.position;
			target.y = this.transform.position.y;
			Vector3 moveDirection = target - this.transform.position; 
			if (Physics.Raycast (this.transform.position, moveDirection, out hit, range)) {
				return hit.collider.tag == "Player";
			}
		}
		return false;
	}

	void changeState() {
		if (hitTimer < hitTime) {
			hitTimer += Time.deltaTime;
			currentState = (int)states.Hit;
		}
		else if( checkAngle() && canSee() ) currentState = (int)states.Shoot;
		else if( waypointSize > 0 ) currentState = (int)states.Patrol;
	}

	void gotHit() {
		hitTimer = 0f;
		currentState = (int)states.Hit;
	}

	void move() {
		float distance = Vector3.Distance (waypoints [waypointIndex].position, this.transform.position);
		Vector3 target = waypoints [waypointIndex].position;
		target.y = this.transform.position.y;
		Vector3 moveDirection = target - this.transform.position; 
		Quaternion neededRotation = Quaternion.LookRotation (moveDirection);
		if (moveDirection.magnitude > .2) {
			transform.rotation = Quaternion.Slerp(this.transform.rotation, neededRotation, Time.deltaTime * rotationSpeed );
			moveDirection.y -= gravity * Time.deltaTime;
			CharacterController controller = GetComponent<CharacterController>();
			controller.Move(moveDirection.normalized * speed * Time.deltaTime);
		} else {
			transform.position = target;
			waypointIndex++;
			waypointIndex %= waypointSize;
		}
	}

	void shoot() {
		if( fireTimer < fireRate ) {
			return;
		}
		fireSoundSource.Play();
		Vector3 target = player.transform.position;
		target.y = this.transform.position.y;
		Vector3 moveDirection = target - this.transform.position; 
		Quaternion neededRotation = Quaternion.LookRotation (moveDirection);
		transform.rotation = Quaternion.Slerp(this.transform.rotation, neededRotation, Time.deltaTime * rotationSpeed );
		RaycastHit hit;
		if (Physics.Raycast (this.transform.position, this.transform.forward, out hit, range)) {
			if (hit.collider.tag == "Player") {
				player.BroadcastMessage ("ApplyDamage", damage);
			}
		}
		fireTimer = 0f;
	}

	// Update is called once per frame
	void Update () {
		Vector3 moveDirection = new Vector3 (0f, 0f, 0f);
		moveDirection.y -= gravity * Time.deltaTime;
		CharacterController controller = GetComponent<CharacterController>();
		controller.Move(moveDirection.normalized * speed * Time.deltaTime);

		changeState ();

		anim.SetInteger ( "currentState", currentState );

		if (fireTimer < fireRate) {
			fireTimer += Time.deltaTime;
		} 

		if (currentState == (int)states.Patrol) {
			move ();
		}
		else if( currentState == (int)states.Idle ) {
			
		}
		else if( currentState == (int)states.Shoot ) {
			shoot ();
		}
	}
}
