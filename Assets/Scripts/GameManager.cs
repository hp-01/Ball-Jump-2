using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject sphere;
	public static GameManager instance;
	private int previousScore = 0;
	public Text scoreText;
	public GameObject fadeOut;

	public void SetScoreText()
	{
		scoreText.text = "" + previousScore;
	}

	public int health = 100;
	public Text healthText;
	public void SetHealthText()
	{
		healthText.text = "" +health;
	}

	public void Reset()
	{
		previousScore = 0;
		health = 100;
		isAlive = true;
	}
	public bool isAlive = true;
    // Start is called before the first frame update
    void Awake()
    {
		instance = this;
		EventManager eventManager = sphere.GetComponent<EventManager>();
		eventManager.OnRestart += EventManager_OnRestart;
    }

	private void EventManager_OnRestart(object sender, EventManager.OnRestartEventArgs e)
	{
		Reset();
	}

	private void Update()
	{
		isAlive = sphere.GetComponent<PlayerController>().isAlive;
		health = sphere.GetComponent<PlayerController>().health;
		previousScore = sphere.GetComponent<PlayerController>().score;
		SetHealthText();
		SetScoreText();
	}

	public void OnClick_Menu()
	{
		StartCoroutine(Menu());
	}

	IEnumerator Menu()
	{
		fadeOut.SetActive(true);
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(0);
	}
}
