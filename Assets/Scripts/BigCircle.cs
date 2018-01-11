using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCircle : MonoBehaviour
{
	public int redsHigh;
	public float alpha;
	public bool active = true;
	public float t = 0.0f;

	void Start()
	{
		float develop = redsHigh/3000.0f;

		transform.localScale *= develop;
	}

	void Update()
	{
		if(active == true)
		{
			if(this.GetComponent<SpriteRenderer>().color.a < 1f)
			{
				this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, Mathf.Lerp(alpha, 1f, t));
			}
			else
			{
				this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
			}
		}
		else
		{
			if(this.GetComponent<SpriteRenderer>().color.a > alpha)
			{
				this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, Mathf.Lerp(1f, alpha, t));
			}
			else
			{
				this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, alpha);
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.GetComponent<Transform>().childCount > redsHigh)
		{
			active = false;
		}
	}
}
