using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
	public static PlatformManager instance;
	public List<GameObject> platforms = new List<GameObject>();
	public GameObject player;
	private float time = 4;
	private Vector3 target = new Vector3(0, 0, 4);

	void Awake()
	{
		instance = this;
		EventManager eventManager = player.GetComponent<EventManager>();
		eventManager.OnRestart += EventManager_OnRestart;
		foreach (GameObject platform in platforms)
		{
			platform.SetActive(false);
		}
		GetPlatforms(target);
	}

	private void EventManager_OnRestart(object sender, EventManager.OnRestartEventArgs e)
	{
		DisableAll();
		target = Vector3.zero;
		GetPlatforms(target);
		time = 0;
	}

	private void Update()
	{
		time -= Time.deltaTime;
		if(time < 0f)
		{
			target.z += 20;
			GetPlatforms(target);
			time = 4f;
		}
	}

	public GameObject GetPlatforms(Vector3 position)
	{
		List<GameObject> platform = new List<GameObject>();
		for(int i=0;i<platforms.Count;i++)
		{
			if(!platforms[i].activeInHierarchy)
			{
				platform.Add(platforms[i]);
			}
		}
		int rand = UnityEngine.Random.Range(0, platform.Count);
		GameObject obj = platforms[rand];
		obj.transform.position = position;
		obj.SetActive(true);
		return obj;
	}

	public void DisableAll()
	{
		foreach (GameObject platform in platforms)
		{
			platform.SetActive(false);
		}
	}
}
