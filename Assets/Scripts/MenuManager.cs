using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public static MenuManager instance;
	public GameObject fadeOut;

	private void Awake()
	{
		if(instance != null && instance == this)
		{
			Destroy(gameObject);
		}
		instance = this;
	}

	public void OnStart()
	{
		StartCoroutine(OnStartRoutine());
		SceneManager.LoadScene("Main");
	}

	IEnumerator OnStartRoutine()
	{
		fadeOut.SetActive(true);
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(1);
	}
}
