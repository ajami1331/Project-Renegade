using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterMovement : MonoBehaviour {
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public GameObject cameraObject;
    private Vector3 moveDirection = Vector3.zero;

	void Awake() {
		cameraObject = GameObject.FindWithTag("MainCamera");
	}
    
	void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        this.transform.rotation = Quaternion.Euler(0, cameraObject.GetComponent<MouseLook>().currentYRotation, 0);
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (CrossPlatformInputManager.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
