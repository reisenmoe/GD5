using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public PlayerController controller;

	//The camera component itself
	public Camera myCamera;
	//The transform component of the camera itself
	public Transform cameraTransform;

	//Additional camera used for first person view only.
	public Camera firstPersonCamera;

	//The 3D gun model used while in first person view mode.
	public Transform firstPersonGun;

	//The parent of the camera
	public Transform cameraHolder;

	//Rotation speed of the camera
	public float cameraPanSpeed;

	//Are we aiming the sight?
	private bool isAimingDownSight;

	//Used for limiting the max vertical rotate amount of the camera.
	private const float maxCameraRotY = 80f;
	private const float minCameraRotY = 360f - 70f;


	void Awake()
	{
		//Don't aim down at the start
		AimSight(false);
	}

	void Update()
	{
		//If pressed the right mouse button
		if(Input.GetMouseButtonDown(1))
		{
			AimSight( !isAimingDownSight );
		}
	}

	void LateUpdate()
	{
		//Get deltatime
		float deltaTime = Time.deltaTime;

		//Receive inputs
		float horizontal = Input.GetAxisRaw("Mouse X");
		float vertical = Input.GetAxisRaw ("Mouse Y");

		//If cursor is locked to the center
		if(Cursor.lockState == CursorLockMode.Locked)
		{
			//Do camera panning
			DoCameraPan(horizontal, vertical, deltaTime);
		}
	}

	#region Mouse panning
	void DoCameraPan(float horizontal, float vertical, float deltaTime)
	{
		//Get current camera view rotation
		Vector3 rot = cameraHolder.eulerAngles;

		//Apply vertical panning
		rot.x -= vertical * deltaTime * cameraPanSpeed;
		//Clamp to make sure camera doesn't rotate too much
		if (rot.x > maxCameraRotY && rot.x < 180f)
			rot.x = maxCameraRotY;
		else if (rot.x < minCameraRotY && rot.x > 180f)
			rot.x = minCameraRotY;

		//Apply horizontal panning
		rot.y += horizontal * deltaTime * cameraPanSpeed;

		//Remove z axis rotation
		rot.z = 0f;

		//Set rotation
		cameraHolder.localRotation = Quaternion.Euler(rot.x, 0f, 0f);
		//Set character rotation
		transform.rotation = Quaternion.Euler(0f, rot.y, 0f);
		//Set effect holder's view rotation
		//effectHolder.localRotation = Quaternion.Euler(rot.x, 0f, 0f);//rot.y, 0f);
	}
	#endregion

	#region Aiming
	void AimSight(bool doAim)
	{
		//Set flag
		isAimingDownSight = doAim;

		//If aiming
		if(doAim)
		{
			//Set gun position
			firstPersonGun.localPosition = new Vector3(0.003f, -0.257f, 0.3f);

			//Set camera field of view
			myCamera.fieldOfView = 30;
			firstPersonCamera.fieldOfView = 30;
		}
		else
		{
			//Set gun position
			firstPersonGun.localPosition = new Vector3(0.345f, -0.43f, 0.608f);

			//Set camera field of view
			myCamera.fieldOfView = 60;
			firstPersonCamera.fieldOfView = 60;
		}
	}
	#endregion
}
