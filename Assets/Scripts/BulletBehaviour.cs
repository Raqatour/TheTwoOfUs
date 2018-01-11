using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	public float speed = 50.0f;
	public Transform target;

	private Rigidbody rbBullet;

	void Start()
	{
		rbBullet = this.GetComponent<Rigidbody>();
	}

	void Update()
	{
		rbBullet.MovePosition(transform.position + transform.right * speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Wall")
		{
			Destroy(this.gameObject);
		}

		if(other.collider.tag == "Enemy")
		{
			Destroy(this.gameObject);
		}

		if(other.collider.tag == "Ball")
		{
			Destroy(this.gameObject);
		}
	}
}
