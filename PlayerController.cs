using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController {

	public PlayerEntity owner;
	public CameraManager camManager;

	public CharacterController controller;

	public float jumpForce;
	private float curVerticalVelocity;
	private const float gravity = 9.8f;


	protected override void Awake()
	{
		//Do BaseController's Awake first
		base.Awake();

		curVerticalVelocity = 0f;
	}

	void Update ()
	{
		//Return if dead
		if (owner.IsDead)
			return;

		//Lock the cursor
		if(Input.GetKeyDown(KeyCode.LeftAlt))
		{
			if(Cursor.lockState == CursorLockMode.Locked)
				Cursor.lockState = CursorLockMode.None;
			else
				Cursor.lockState = CursorLockMode.Locked;
		}

		//Store deltatime for performance
		float deltaTime = Time.deltaTime;

		//Receive inputs
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		//Normalize it
		if(inputX != 0f && inputY != 0f)
		{
			inputX *= 0.707106f;
			inputY *= 0.707106f;
		}

		bool isPressingJumpKey = Input.GetKey (KeyCode.Space);

		#region Moving the character
		//Calculate movement vector
		Vector3 movementVector = new Vector3(inputX, 0f, inputY);

		//Calculate the direction vector
		Vector3 direction = camManager.cameraHolder.TransformDirection(movementVector);

		//If pressing the left shift
		if(Input.GetKey(KeyCode.LeftShift))
		{
			//Apply 1.5 times faster move speed
			direction *= moveSpeed * 1.5f;
		}
		else
		{
			//Apply move speed
			direction *= moveSpeed;
		}

		//If the controller is on the ground
		if(controller.isGrounded)
		{
			//If pressing the jump button
			if(isPressingJumpKey)
			{
				curVerticalVelocity = jumpForce;
			}
			//If not
			else
			{
				//Reset the jump velocity
				curVerticalVelocity = 0f;
			}
		}

		//Apply gravity
		curVerticalVelocity -= gravity * deltaTime;

		//Apply jump force
		direction.y = curVerticalVelocity;

		//Move the controller
		controller.Move(direction * deltaTime);
		#endregion
	}
}
