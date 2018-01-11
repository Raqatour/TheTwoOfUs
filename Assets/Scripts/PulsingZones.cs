using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PulsingZones : MonoBehaviour
{
	public float delta = 75.0f;
	public float speed = 1.0f;

	void Update()
	{
		GetComponent<Light>().intensity += delta * Mathf.Sin(Time.time * speed);

		/*if(GetComponent<Light>().intensity > 75f)
		{
			GetComponent<SphereCollider>().enabled = false;
		}
		else
		{
			GetComponent<SphereCollider>().enabled = true;
		}*/

	}

	/*void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Orga" || other.collider.tag == "Mecha")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}*/
}
