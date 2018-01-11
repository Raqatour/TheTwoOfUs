using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gazer : MonoBehaviour
{
	public int life;
	public float lifeTime;

	private float timerLife = 0.0f;
	private bool resize = true;

	void Update()
	{
		if(transform.eulerAngles.z == 0)
		{
			RaycastHit hit;
			Ray range = new Ray(transform.position, Vector3.left);

			if(Physics.Raycast(range, out hit, 2000))
			{
				if(hit.collider.tag == "Wall")
				{
					if(resize)
					{
						transform.localScale += new Vector3(1f * hit.distance/5000, 0f, 0f);
					}

					if(!resize)
					{
						transform.localScale -= new Vector3(1f * hit.distance/5000, 0f, 0f);
					}
				}
			}
		}

		if(transform.eulerAngles.z == 90)
		{
			RaycastHit hit;
			Ray range = new Ray(transform.position, Vector3.down);

			if(Physics.Raycast(range, out hit, 2000))
			{
				if(hit.collider.tag == "Wall")
				{
					if(resize)
					{
						transform.localScale += new Vector3(1f * hit.distance/5000, 0f, 0f);
					}

					if(!resize)
					{
						transform.localScale -= new Vector3(1f * hit.distance/5000, 0f, 0f);
					}
				}
			}
		}

		if(transform.eulerAngles.z == 270)
		{
			RaycastHit hit;
			Ray range = new Ray(transform.position, Vector3.up);

			if(Physics.Raycast(range, out hit, 2000))
			{
				if(hit.collider.tag == "Wall")
				{
					if(resize)
					{
						transform.localScale += new Vector3(1f * hit.distance/5000, 0f, 0f);
					}

					if(!resize)
					{
						transform.localScale -= new Vector3(1f * hit.distance/5000, 0f, 0f);
					}
				}
			}
		}

		if(transform.eulerAngles.z == 180)
		{
			RaycastHit hit;
			Ray range = new Ray(transform.position, Vector3.right);
			if(Physics.Raycast(range, out hit, 2000))
			{
				Debug.Log(hit.collider.name);
				if(hit.collider.tag == "Wall")
				{
					if(resize)
					{
						transform.localScale += new Vector3(1f * hit.distance/5000, 0f, 0f);
					}

					if(!resize)
					{
						transform.localScale -= new Vector3(1f * hit.distance/5000, 0f, 0f);
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Wall")
		{
			resize = false;
		}

		if(other.GetComponent<Collider>().tag == "Mecha" || other.GetComponent<Collider>().tag == "Orga")
		{
			Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.GetComponent<Collider>().tag  == "Wall")
		{
			resize = false;
		}	
		if(other.GetComponent<Collider>().tag  == null)
		{
			resize = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.GetComponent<Collider>().tag  == "Wall")
		{
			resize = true;
		}	
	}
}
