using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

	public Animator animator;


	void Awake()
	{
		//Do idle animation on start
		IdleAnimation();
	}

	public void IdleAnimation()
	{
		if(!IsAttacking)
			animator.SetInteger ("state", 0);
	}

	public void AttackAnimation()
	{
		animator.SetInteger ("state", 1);
	}
	public void AttackAnimation_End()
	{
		animator.SetInteger ("state", 0);
	}
	public bool IsAttacking
	{
		get
		{
			return animator.GetInteger ("state") == 1;
		}
	}

	public void MoveAnimation()
	{
		if (!IsAttacking)
			animator.SetInteger ("state", 2);
	}

	public void DeathAnimation()
	{
		animator.SetInteger("state", 3);
	}
}
