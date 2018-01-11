using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
	public bool filled = false;

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Enemy")
		{
			filled = true;
			Debug.Log("Hit");
		}

		if(filled && other.collider.tag == "Bullet")
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Enemy")
		{
			filled = true;
			Debug.Log("Hit");
		}

		if(other.GetComponent<Collider>().tag == "Bullet")
		{
			Destroy(this.gameObject);
		}
	}
}
