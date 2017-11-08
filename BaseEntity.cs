using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour {

	public int		currentHealth;
	public int		maxHealth;


	protected virtual void Awake()
	{
		//Set initial health
		currentHealth = maxHealth;
	}

	#region Properties
	public bool IsDead
	{
		get
		{
			//If health is zero or lower, the entity is dead!
			return currentHealth <= 0;
		}
	}
	#endregion

	#region Public methods
	public virtual void DoDamage(int damage)
	{
		//Subtract health by damage
		currentHealth -= damage;
	}
	#endregion
}
