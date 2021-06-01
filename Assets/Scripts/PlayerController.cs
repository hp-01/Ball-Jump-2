using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private Rigidbody body;
	private Vector3 target;
	public float gravity = -18f;
	public float height = 2f;
	public float horizontalDistance = 2f;
	public GameObject platform;
	public GameObject checkingPoint;
	public int health = 100;
	public bool isAlive = true;
	public int score = 0;
	public AudioSource bounce;
	public ParticleSystem particle;

	// Start is called before the first frame update
    void Start()
    {
		particle.Stop();
		target = Vector3.zero;
		EventManager eventManager = GetComponent<EventManager>();
		eventManager.OnRestart += EventManager_OnRestart;
		body = GetComponent<Rigidbody>();
		Physics.gravity = Vector3.up * gravity;
	}

	private void EventManager_OnRestart(object sender, EventManager.OnRestartEventArgs e)
	{
		isAlive = true;
		transform.position = Vector3.zero + Vector3.up * 1;
		platform.transform.position = Vector3.zero;
		target = Vector3.zero;
		health = 100;
		score = 0;
	}

	float getMorePlatform = 0;
	// Update is called once per frame
	void Update()
    {
		if(health <= 0)
		{
			isAlive = false;
			health = 100;
		}
		/*if (Input.GetKeyDown(KeyCode.Space))
		{
			CheckPlatform();
		}*/
		if(Input.touchCount == 1)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				CheckPlatform();
			}
		}
    }

	private void CheckPlatform()
	{

		if (Physics.Raycast(checkingPoint.transform.position, Vector3.forward, out RaycastHit hit, 5))
		{
			if (hit.transform.CompareTag("Platform"))
			{
				health -= 35;
				health = Mathf.Clamp(health, 0, 100);
			}
		}
		platform.transform.position = target;
	}

	Vector3 CalculateLaunchVelocity()
	{
		float displacementY = target.y - body.position.y;
		Vector3 displacementXZ = new Vector3(target.x - body.position.x, 0,
			target.z - body.position.z);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
		Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));
		return velocityXZ + velocityY * -Mathf.Sign(gravity);
	}

	private bool jump = true;
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Platform") && jump)
		{
			jump = false;
			target.z += horizontalDistance;
			score += 10;
			body.velocity = CalculateLaunchVelocity();
			bounce.Play();
			StartCoroutine(BounceParticle());
		}
		if(collision.gameObject.CompareTag("Finish"))
		{
			isAlive = false;
			health = 0;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("Platform") && !jump)
		{
			jump = true;
			target.z += horizontalDistance;
			body.velocity = CalculateLaunchVelocity();
		}
	}

	WaitForSeconds wait = new WaitForSeconds(0.3f);
	IEnumerator BounceParticle()
	{
		particle.Play();
		yield return wait;
		particle.Stop();
	}
}
