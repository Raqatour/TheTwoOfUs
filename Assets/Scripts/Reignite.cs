using System.Collections;
using System.Collections.Generic;
using TwoOfUs.Player;
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
		Creator otherCreator = other.GetComponent<Creator>();
		
		if(other.CompareTag("Orga"))
		{
			isButtonUp = true;
			otherCreator.soulMate.GetComponent<Creator>().timerSqueeze1 = 5;
			otherCreator.IsOrgaGlowing = true;
			otherCreator.soulMate.GetComponent<Creator>().IsMechaGlowing = true;
			otherCreator.IsIgnited = true;
			otherCreator.soulMate.GetComponent<Creator>().IsIgnited = true;
			if(!aud.isPlaying)
			{
				aud.PlayOneShot(intake, 1.0f);
			}
		}

		if(other.CompareTag("Mecha"))
		{
			isButtonUp = true;
			otherCreator.soulMate.GetComponent<Creator>().timerSqueeze0 = 5;
			otherCreator.IsMechaGlowing = true;
			otherCreator.soulMate.GetComponent<Creator>().IsOrgaGlowing = true;
			otherCreator.IsIgnited = true;
			otherCreator.soulMate.GetComponent<Creator>().IsIgnited = true;
			if(!aud.isPlaying)
			{
				aud.PlayOneShot(intake, 1.0f);
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Orga"))
		{
			if(other.GetComponent<Creator>().gamePad2.RightTrigger == 0 && isButtonUp)
			{
				other.GetComponent<Creator>().timerSqueeze0 = 0;
				other.GetComponent<Creator>().soulMate.GetComponent<Creator>().timerSqueeze1 = 0;
				isButtonUp = false;
			}
		}

		if(other.CompareTag("Mecha"))
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
