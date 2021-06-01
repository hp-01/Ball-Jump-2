using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
	public GameObject player;

	private void Update()
	{
		if(player.transform.position.z  > (transform.position.z + 25))
		{
			gameObject.SetActive(false);
		}
	}
}
