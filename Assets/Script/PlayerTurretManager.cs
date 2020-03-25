using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerTurretManager : MonoBehaviour {

    // Use this for initialization
	// -12.63947 2.080002 -13.27607
    public GameObject dummyTurret;
    public GameObject liveTurret;
    public GameObject Player;
	public GameObject cameraObj;
	public MouseLook mouse;
	public bool active;
	public GameObject machineGunOff;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
		GameObject cameraObj = GameObject.FindWithTag("MainCamera");
		mouse = cameraObj.GetComponent < MouseLook > ();
    }

    void Start ()
	{
		active = false;
	}

	IEnumerator Switch() {
		yield return new WaitForSeconds ( .5f );
	}

	void Active() {
		active = true;
	}

	// Update is called once per frame
	void Update () 
	{
		if(CrossPlatformInputManager.GetButtonDown( "Function" ) && active )
        {
			active = false;
			StartCoroutine( Switch() );
			//Debug.Log ("a");
			Player.transform.position = machineGunOff.transform.position;
			mouse.yRotation = 0f;
			Player.SetActive(true);
			//Player.transform.Rotate ( new Vector3( 0f, 180f, 0f ) );
			//Debug.Log ("b");
			dummyTurret.SetActive(true);
			//Debug.Log ("c");
			liveTurret.SetActive(false);
			//Debug.Log ("d");
			GameObject.FindWithTag ("Manager").BroadcastMessage ( "changeIcon" );

			//Debug.Log ("e");
        }
	}
}
