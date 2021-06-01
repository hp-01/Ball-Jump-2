using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
	public GameObject player;
	public Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
		distance = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector3(transform.position.x, transform.position.y, distance.z + player.transform.position.z);
    }
}
