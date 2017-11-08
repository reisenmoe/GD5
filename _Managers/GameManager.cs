using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager Instance;

	public HUDManager hudManager;

	public int currentScore { get; private set; }
	public int currentKills { get; private set; }

	public static bool IsOnGoing
	{
		get
		{
			return !PlayerEntity.Instance.IsDead;
		}
	}


	void Awake()
	{
		Instance = this;

		//Reset the game
		ResetGame();
	}

	void Update()
	{
		//If the player is dead
		if(PlayerEntity.Instance.IsDead)
		{
			//Unlock the cursor
			Cursor.lockState = CursorLockMode.None;
			//Show the game over screen.
			hudManager.ToggleGameOverPanel(true);
		}
	}

	#region Button events
	public void OnGameOver_Restart()
	{
		//Reset enemy spawned count
		EnemySpawner.ResetSpawnCount();
		//Reload the scene
		SceneManager.LoadScene(0);
	}
	public void OnGameOver_Quit()
	{
		Application.Quit();
	}
	#endregion

	#region Instance methods
	void ResetGame()
	{
		//Reset scores
		currentScore = 0;
		hudManager.SetScoreText(0);

		//Reset kills
		currentKills = 0;
		hudManager.SetKillsText(0);
	}
	#endregion

	#region Static methods
	public static void AddScore(int amount)
	{
		//Increase current score
		Instance.currentScore += amount;

		//Refresh the label
		Instance.hudManager.SetScoreText( Instance.currentScore );
	}
	public static void AddKill()
	{
		//Increment kill count
		Instance.currentKills ++;

		//Refresh the label
		Instance.hudManager.SetKillsText( Instance.currentKills );
	}
	#endregion

}
