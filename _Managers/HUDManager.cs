using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	
	public Text scoreText;

	public Text killsText;

	public Slider healthBar;

	public Image damagedEffect;

	public GameObject gameOverPanel;


	void Awake()
	{
		//Reset things
		SetScoreText(0);
		SetKillsText(0);
		SetHealthBar(1f);
		ToggleGameOverPanel(false);
	}

	#region Public methods
	public void SetScoreText(int score)
	{
		//Set score text
		scoreText.text = "Score: " + score.ToString();
	}
	public void SetKillsText(int kills)
	{
		//Set kills text
		killsText.text = "Kills: " + kills.ToString();
	}
	public void SetHealthBar(float value)
	{
		//Set healthbar fill amount
		healthBar.value = value;
	}
	public void ShowDamagedEffect()
	{
		//Set damaged effect image's alpha so it's visible
		damagedEffect.color = new Color(1f, 0f, 0f, 0.25f);
	}
	public void ToggleGameOverPanel(bool enable)
	{
		//Set GameOverPanel's active state
		gameOverPanel.SetActive(enable);
	}
	#endregion

	void Update()
	{
		//Decrease alpha value
		Color color = damagedEffect.color;
		color.a -= Time.deltaTime;
		damagedEffect.color = color;
	}
}
