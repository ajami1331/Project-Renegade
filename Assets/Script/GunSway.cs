using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GunSway : MonoBehaviour {

    public float amount;
    public float maxAmount;
    public float smoothAmount;
    public float movementX;
    public float movementY;

    private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
        initialPosition = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        movementX = -CrossPlatformInputManager.GetAxis("Mouse X") * amount;
        movementY = -CrossPlatformInputManager.GetAxis("Mouse Y") * amount;

        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0.0f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}
