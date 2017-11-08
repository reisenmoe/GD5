using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : BaseAttacker {

	public EnemyEntity owner;


	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Update ()
	{
		base.Update ();

		//If dead, return
		if(owner.IsDead)
			return;

		//If attack cooltime is gone
		if (CanAttack)
		{
			//If close enough to attack
			if (owner.controller.IsInAttackRange ())
			{
				//Set cooltime
				SetAttackCooltime();

				//Do animation
				owner.animator.AttackAnimation();
			}
		}
	}

	//This method will be called from the animation.
	public void OnEnemyDoAttack()
	{
		//Do damage on player entity
		PlayerEntity.Instance.DoDamage( damage );
	}
}
