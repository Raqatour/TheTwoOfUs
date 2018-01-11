using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCircle : MonoBehaviour
{
	public int bluesLow;
	public float alpha;
	public bool active = true;
	public float t = 0.0f;

	void Start()
	{
		float develop = bluesLow/3000.0f;

		transform.localScale *= develop;
	}

	void Update()
	{
		if(active == true)
		{
			if(this.GetComponent<SpriteRenderer>().color.a < 1f)
			{
				this.GetComponent<SpriteRenderer>().color = new Color(0f, 0.68f, 1f, Mathf.Lerp(alpha, 1f, t));
			}
			else
			{
				this.GetComponent<SpriteRenderer>().color = new Color(0f, 0.68f, 1f, 1f);
			}
		}
		else
		{
			if(this.GetComponent<SpriteRenderer>().color.a > alpha)
			{
				this.GetComponent<SpriteRenderer>().color = new Color(0f, 0.68f, 1f, Mathf.Lerp(1f, alpha, t));
			}
			else
			{
				this.GetComponent<SpriteRenderer>().color = new Color(0f, 0.68f, 1f, alpha);
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.GetComponent<Transform>().childCount < bluesLow)
		{
			active = false;
		}
	}
}
