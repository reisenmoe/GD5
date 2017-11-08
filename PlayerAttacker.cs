using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : BaseAttacker {

	public PlayerEntity owner;

	public AudioSource fireAudio;

	public Light fireLight;
	public ParticleSystem bulletShot;
	public ParticleSystem bulletFire;
	private float bulletAliveTime;
	private const float maxBulletAliveTime = 0.05f;

	public Light flashlight;

	public Camera myCamera;

	public Transform bulletSource;


	protected override void Awake()
	{
		bulletAliveTime = 0f;

		//Disable lights
		fireLight.enabled = false;
	}

	#region Overriding methods
	protected override void Update ()
	{
		//Do BaseAttacker's Update first
		base.Update ();

		//Update bullet
		UpdateBullet();

		//If player inputs the shoot key
		if (Input.GetMouseButton (0))
		{
			//If player can attack
			//And cursor is locked to center
			if (CanAttack &&
				Cursor.lockState == CursorLockMode.Locked)
			{
				//Set cooltime
				SetAttackCooltime();

				//Show the bullet shot
				ShowBullet();

				//Play audio
				fireAudio.Stop();
				fireAudio.Play();

				//Do a raycast from bullet source
				RaycastHit hit;
				if (Physics.Raycast (myCamera.ScreenPointToRay (new Vector3(Screen.width*0.5f, Screen.height*0.5f, 0f)), out hit, attackRange, targetLayer.value))
				{
					//Try to get the enemy entity object
					EnemyEntity enemy = hit.collider.GetComponent<EnemyEntity>();

					//If it is a valid enemy
					if (enemy != null)
					{
						//Damage the enemy
						enemy.DoDamage (damage);
					}
				}
			}
		}

		//If player presses the flashlight toggle key
		if(Input.GetKeyDown(KeyCode.Z))
		{
			//Toggle light enable state
			flashlight.enabled = !flashlight.enabled;
		}
	}
	#endregion

	#region Other methods
	void UpdateBullet()
	{
		//Decrease bullet alive time
		bulletAliveTime -= Time.deltaTime;

		//If bullet alive time is zero or lower
		if (bulletAliveTime <= 0f)
		{
			//Hide the light
			fireLight.enabled = false;
		}
	}
	void ShowBullet()
	{
		//Set bullet alive time
		bulletAliveTime = maxBulletAliveTime;

		//Show bullet shot
		bulletShot.Play();
		bulletFire.Play();

		//Show light
		fireLight.enabled = true;
	}
	#endregion
}
