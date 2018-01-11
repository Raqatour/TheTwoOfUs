using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpermTailMovement : MonoBehaviour
{
	public float delta = 1.5f; //Amount to move up and down by from start position
	public float speed = 2.0f;

	void Update()
	{
		Vector3 v = transform.parent.position;
		v.y += delta * Mathf.Sin(Time.time * speed);
		v.x += delta * Mathf.Sin(Time.time * speed);
		transform.position = v;

		/*if(GetComponentInParent<Creator>().ammo != 1)
		{
			GetComponent<TrailRenderer>().enabled = false;
		}
		else
		{
			GetComponent<TrailRenderer>().enabled = true;
		}*/
	}
}
