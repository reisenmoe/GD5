using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttacker : MonoBehaviour {

	public LayerMask targetLayer;

	public int damage;

	public float currentAttackDelay;
	public float attackSpeed;

	public float attackRange;


	protected virtual void Awake()
	{
		//Set initial attack delay
		currentAttackDelay = 0f;
	}

	#region Properties
	public bool CanAttack
	{
		get
		{
			//The entity can attack if delay is zero or less.
			return currentAttackDelay <= 0f;
		}
	}
	#endregion

	#region Public methods
	public void SetAttackCooltime()
	{
		//Set attack delay
		currentAttackDelay = 1f / attackSpeed;
	}
	#endregion

	#region Virtual methods
	protected virtual void Update()
	{
		//Decrease attack delay
		currentAttackDelay -= Time.deltaTime;
	}
	#endregion
}
