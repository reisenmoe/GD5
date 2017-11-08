using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntity : BaseEntity {

	public static PlayerEntity Instance;

	public HUDManager hudManager;

	public PlayerController controller;
	public PlayerAttacker attacker;


	protected override void Awake()
	{
		//Only one player exists.
		//Other scripts can access this entity by Calling PlayerEntity.Instance;
		Instance = this;
	}

	public override void DoDamage (int damage)
	{
		base.DoDamage (damage);

		//Refresh slider
		hudManager.SetHealthBar((float)currentHealth / (float)maxHealth);

		//Show damaged effect
		hudManager.ShowDamagedEffect();
	}
}
