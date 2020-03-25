using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerTurret : MonoBehaviour
{

    public bool invertMouse = true;
    public float lookSensitivity = 5.0f;
    [HideInInspector]
    public float xRotation;
    [HideInInspector]
    public float yRotation;
    [HideInInspector]
    public float currentXRotation;
    [HideInInspector]
    public float currentYRotation;
    [HideInInspector]
    public float xRotationV;
    [HideInInspector]
    public float yRotationV;
    [HideInInspector]
    public float lookSmoothDamp = 0.1f;

	public float xLowerBound = 270.0f;
	public float xUpperBound = 285.0f;
	
	public float yLowerBound = -15.0f;
	public float yUpperBound = 15.0f;
		
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        xRotation += CrossPlatformInputManager.GetAxis("Mouse Y") * lookSensitivity * (invertMouse == true ? -1.0f : 1.0f);
        yRotation += CrossPlatformInputManager.GetAxis("Mouse X") * lookSensitivity;

		xRotation = Mathf.Clamp(xRotation, xLowerBound, xUpperBound);
		yRotation = Mathf.Clamp(yRotation, yLowerBound, yUpperBound);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0.0f);
    }

}
//Previous rotations
// x axis: 270.0f, 285.0f
// y axis: -15.0f, 15.0f