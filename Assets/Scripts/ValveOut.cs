using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveOut : MonoBehaviour
{
	bool isParentEntered = false;
	public AudioSource aud;
	public AudioClip slurp;
	public AudioClip beep;

	void Start()
	{
		aud = GetComponent<AudioSource>();
	}

	void Update()
	{
		isParentEntered = GetComponentInParent<Valve>().isEntered;

		if(isParentEntered)
		{
			this.GetComponent<BoxCollider>().isTrigger = true;
		}
		else
		{
			this.GetComponent<BoxCollider>().isTrigger = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga" || other.GetComponent<Collider>().tag == "Mecha")
		{
			GetComponentInParent<Valve>().isEntered = false;
			if(!aud.isPlaying)
			{
				aud.PlayOneShot(slurp, 1.0f);
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Orga" || other.collider.tag == "Mecha")
		{
			if(!aud.isPlaying)
			{
				aud.PlayOneShot(beep, 1.0f);
			}
		}
	}
}
