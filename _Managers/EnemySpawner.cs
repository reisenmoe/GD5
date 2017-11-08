using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	private float spawnDelay;

	//For managing spawns
	public static int spawnedCount;
	private const int maxSpawnCount = 10;


	void Update()
	{
		//While the game is on-going
		if(!GameManager.IsOnGoing)
			return;

		//Decrease spawn delay
		spawnDelay -= Time.deltaTime;

		//If time to spawn
		if(spawnDelay <= 0f)
		{
			//Set new spawn delay time
			spawnDelay = Random.Range(4f, 8f);

			//If spawned count is at its limit, return
			if(spawnedCount >= maxSpawnCount)
				return;

			//Increase spawn count
			spawnedCount ++;
			
			//Instantiate a new enemy object
			EnemyEntity enemy = GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<EnemyEntity>();

			//Set random move speed
			enemy.controller.moveSpeed = Random.Range(2f, 4f);
			//Set random damage
			enemy.attacker.damage = Random.Range(10, 18);
		}
	}

	//Editor debugging purposes
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, 0.5f);
	}

	public static void ResetSpawnCount()
	{
		//Resetting spawned entity count
		spawnedCount = 0;
	}
}
