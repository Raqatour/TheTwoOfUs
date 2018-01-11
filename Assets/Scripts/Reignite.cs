using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reignite : MonoBehaviour
{
	bool isButtonUp;
	public AudioSource aud;
	public AudioClip intake;

	void Start()
	{
		aud = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga")
		{
			isButtonUp = true;
			other.GetComponent<Creator>().soulMate.GetComponent<Creator>().timerSqueeze1 = 5;
			other.GetComponent<Creator>().isOrgaGlowing = true;
			other.GetComponent<Creator>().soulMate.GetComponent<Creator>().isMechaGlowing = true;
			other.GetComponent<Creator>().isIgnited = true;
			other.GetComponent<Creator>().soulMate.GetComponent<Creator>().isIgnited = true;
			if(!aud.isPlaying)
			{
				aud.PlayOneShot(intake, 1.0f);
			}
		}

		if(other.GetComponent<Collider>().tag == "Mecha")
		{
			isButtonUp = true;
			other.GetComponent<Creator>().soulMate.GetComponent<Creator>().timerSqueeze0 = 5;
			other.GetComponent<Creator>().isMechaGlowing = true;
			other.GetComponent<Creator>().soulMate.GetComponent<Creator>().isOrgaGlowing = true;
			other.GetComponent<Creator>().isIgnited = true;
			other.GetComponent<Creator>().soulMate.GetComponent<Creator>().isIgnited = true;
			if(!aud.isPlaying)
			{
				aud.PlayOneShot(intake, 1.0f);
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga")
		{
			if(other.GetComponent<Creator>().gamePad2.RightTrigger == 0 && isButtonUp)
			{
				other.GetComponent<Creator>().timerSqueeze0 = 0;
				other.GetComponent<Creator>().soulMate.GetComponent<Creator>().timerSqueeze1 = 0;
				isButtonUp = false;
			}
		}

		if(other.GetComponent<Collider>().tag == "Mecha")
		{
			if(other.GetComponent<Creator>().gamePad1.RightTrigger == 0 && isButtonUp)
			{
				other.GetComponent<Creator>().timerSqueeze1 = 0;
				other.GetComponent<Creator>().soulMate.GetComponent<Creator>().timerSqueeze0 = 0;
				isButtonUp = false;
			}
		}
	}
}
