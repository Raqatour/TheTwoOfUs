using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
	public AudioClip heartbeat;
	AudioSource aud;

	void Start()
	{
		this.GetComponent<Light>().enabled = false;
		aud = GetComponent<AudioSource>();
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Orga" || other.collider.tag == "Mecha")
		{
			aud.PlayOneShot(heartbeat);
			this.GetComponent<Light>().enabled = true;
			transform.gameObject.tag = "Untagged";
		}
	}
}
