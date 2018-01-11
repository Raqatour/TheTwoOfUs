using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWall : MonoBehaviour
{
	public Vector3 pos;
	public Vector3 scale;

	void Update()
	{
		transform.position = new Vector3(pos.x, pos.y, pos.z);
		transform.localScale = new Vector3(scale.x, scale.y, scale.z);

		if(this.GetComponent<SpriteRenderer>().color.a < 1f)
		{
			transform.GetComponent<BoxCollider>().enabled = false;
		}
		else
		{
			transform.GetComponent<BoxCollider>().enabled = true;
		}

		if(transform.parent.GetComponent<SmallCircle>() != null)
		{
			if(transform.parent.GetComponent<SmallCircle>().active == true)
			{
				if(this.GetComponent<SpriteRenderer>().color.a < 1f)
				{
					this.GetComponent<SpriteRenderer>().color = new Color(0f, 0.68f, 1f, Mathf.Lerp(transform.parent.GetComponent<SmallCircle>().alpha, 1f, transform.parent.GetComponent<SmallCircle>().t));
				}
				else
				{
					this.GetComponent<SpriteRenderer>().color = new Color(0f, 0.68f, 1f, 1f);
				}
			}
			else
			{
				if(this.GetComponent<SpriteRenderer>().color.a > transform.parent.GetComponent<SmallCircle>().alpha)
				{
					this.GetComponent<SpriteRenderer>().color = new Color(0f, 0.68f, 1f, Mathf.Lerp(1f, transform.parent.GetComponent<SmallCircle>().alpha, transform.parent.GetComponent<SmallCircle>().t));
				}
				else
				{
					this.GetComponent<SpriteRenderer>().color = new Color(0f, 0.68f, 1f, transform.parent.GetComponent<SmallCircle>().alpha);
				}
			}
		}

		if(transform.parent.GetComponent<BigCircle>() != null)
		{
			if(transform.parent.GetComponent<BigCircle>().active == true)
			{
				if(this.GetComponent<SpriteRenderer>().color.a < 1f)
				{
					this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, Mathf.Lerp(transform.parent.GetComponent<BigCircle>().alpha, 1f, transform.parent.GetComponent<BigCircle>().t));
				}
				else
				{
					this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
				}
			}
			else
			{
				if(this.GetComponent<SpriteRenderer>().color.a > transform.parent.GetComponent<BigCircle>().alpha)
				{
					this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, Mathf.Lerp(1f, transform.parent.GetComponent<BigCircle>().alpha, transform.parent.GetComponent<BigCircle>().t));
				}
				else
				{
					this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, transform.parent.GetComponent<BigCircle>().alpha);
				}
			}
		}
	}
}
