using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseController {

	public EnemyEntity owner;

	public NavMeshAgent navigationAgent;


	protected override void Awake ()
	{
		//Do BaseController's Awake first
		base.Awake ();

		//Apply move speed on nav agent
		navigationAgent.speed = moveSpeed;
	}

	void Update()
	{
		//If owner is dead
		if (owner.IsDead)
		{
			//Agent should stop
			navigationAgent.isStopped = true;
			return;
		}

		//Return if attacking
		if (owner.animator.IsAttacking)
			return;

		//If distance is not close enough
		if(!IsInAttackRange())
		{
			//Keep moving to the destination
			navigationAgent.isStopped = false;
			navigationAgent.destination = PlayerEntity.Instance.transform.position;

			//Do move animation
			owner.animator.MoveAnimation();
		}
		//If close enough to the player
		else
		{
			//Stop moving
			navigationAgent.destination = transform.position;
			navigationAgent.isStopped = true;

			//Do idle animation
			owner.animator.IdleAnimation();
		}
	}

	#region Public methods
	public bool IsInAttackRange()
	{
		//Get player position
		Vector3 playerPosition = PlayerEntity.Instance.transform.position;
		//Get the distance between this enemy and the player
		float distance = Vector3.Distance( transform.position, playerPosition );

		return distance <= owner.attacker.attackRange;
	}
	#endregion
}
