using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : BaseEntity {

	public EnemyController controller;
	public EnemyAttacker attacker;
	public EnemyAnimator animator;


	#region Public methods
	public override void DoDamage (int damage)
	{
		//Do the base damaging actions first
		base.DoDamage (damage);

		//If enemy is dead
		if (IsDead)
		{
			//Change layer
			gameObject.layer = 2;
			//Increase kill count
			GameManager.AddKill();
			//Increase score
			GameManager.AddScore(Random.Range(100, 120));

			//Decrease spawned count
			EnemySpawner.spawnedCount --;

			//Start death animation coroutine
			StartCoroutine( DeathAnimation() );
		}
	}
	#endregion

	#region Death animation
	IEnumerator DeathAnimation()
	{
		//Prepare some variables
		Vector3 startPos = transform.position;
		Vector3 jumpPos = Vector3.up * 0.5f + startPos;

		//Start animation
		float lerpT = 0f;
		while(lerpT < 1f)
		{
			lerpT += Time.deltaTime * 2f;
			if(lerpT > 1f)
				lerpT = 1f;

			//Rotate
			transform.RotateAround( transform.position, Vector3.forward, lerpT * 90f );

			//Jumping up
			if(lerpT < 0.5f)
			{
				//Go up
				transform.position = Vector3.Slerp( startPos, jumpPos, lerpT * 2f );
			}
			//Falling down
			else
			{
				//Go down
				transform.position = Vector3.Slerp( jumpPos, startPos, lerpT * 2f - 1f );
			}

			yield return null;
		}

		//Destroy this gameobject
		Destroy(gameObject);
	}
	#endregion
}
