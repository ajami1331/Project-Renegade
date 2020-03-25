using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MouseLook : MonoBehaviour {

    public bool invertMouse = true;
    public float lookSensitivity = 5.0f;
    //[HideInInspector]
    public float xRotation;
    //[HideInInspector]
    public float yRotation;
    //[HideInInspector]
    public float currentXRotation;
    //[HideInInspector]
    public float currentYRotation;
    //[HideInInspector]
    public float xRotationV;
    //[HideInInspector]
    public float yRotationV;
    //[HideInInspector]
    public float lookSmoothDamp = 0.1f;

    // Use this for initialization
    void Start ()
    {
        //Cursor.visible = false;
		//xRotation = 0f;
		//yRotation = 0f;
		//currentXRotation = 0f;
		//currentYRotation = 0f;
		//xRotationV = 0f;
		//yRotationV = 0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        xRotation += CrossPlatformInputManager.GetAxis("Mouse Y") * lookSensitivity * ( invertMouse == true ? -1.0f : 1.0f  ) ;
        yRotation += CrossPlatformInputManager.GetAxis("Mouse X") * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0.0f);
    }

    public void SetLookSensitivity( float newValue )
    {
        lookSensitivity = newValue;
    }
}
