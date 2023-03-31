using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
	public Transform target;

	public float speed = 5f;
	public float rotateSpeed = 200f;
	public float duration = 15f;
	private Rigidbody2D rb;
	private GameManager gameManager;
	private MovementController movementController;


	// Use this for initialization
	void Start()
	{
		gameObject.GetComponent<Collider2D>().enabled = true;
		gameManager = FindObjectOfType<GameManager>();
		rb = GetComponent<Rigidbody2D>();
		movementController = FindObjectOfType<MovementController>();
	}
	void Update()
	{
		if (duration > 0)
		{
			duration -= Time.deltaTime;
		}
		if (duration <= 0)
		{
			Destroy(this.gameObject);
			ScoreHandler.missileCount++;
		}
	}

	void FixedUpdate()
	{
		Vector2 direction = (Vector2)target.position - rb.position;
		direction.Normalize();
		float rotateAmount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = -rotateAmount * rotateSpeed;
		rb.velocity = transform.up * speed;
	}
	private void OnCollisionEnter2D(Collision2D obj)
	{
		if (obj.gameObject.tag == "Enemy")
		{
			print("Missile collided");
			movementController.PlayAudio(movementController.audioClips[2]);
			StartCoroutine(gameManager.MissileEffect());
			ScoreHandler.missileBonus += 5;
			Destroy(obj.gameObject);
			Destroy(this.gameObject);
		}
	}
	
}
