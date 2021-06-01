using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingPointController : MonoBehaviour
{
	public GameObject player;
	private Vector3 distance;

	private void Start()
	{
		distance = transform.position - player.transform.position;
	}
	// Update is called once per frame
	void Update()
    {
		transform.position = new Vector3(0, 0, distance.z + player.transform.position.z);
    }
}
