using System.Collections;
using System.Collections.Generic;
using TwoOfUs.Player;
using TwoOfUs.Player.Characters;
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
			(otherCreator as Orga).IsOrgaGlowing = true;
			(otherCreator.soulMate.GetComponent<Creator>() as Mecha).IsMechaGlowing = true;
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
			otherCreator.soulMate.GetComponent<Creator>().ForceFinishTimer();
			(otherCreator as Mecha).IsMechaGlowing = true;
			(otherCreator.soulMate.GetComponent<Creator>() as Orga).IsOrgaGlowing = true;
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
				other.GetComponent<Creator>().ForceFinishTimer(true);
				other.GetComponent<Creator>().soulMate.GetComponent<Creator>().ForceFinishTimer(true);
				isButtonUp = false;
			}
		}

		if(other.CompareTag("Mecha"))
		{
			if(other.GetComponent<Creator>().gamePad1.RightTrigger == 0 && isButtonUp)
			{
				other.GetComponent<Creator>().ForceFinishTimer(true);;
				other.GetComponent<Creator>().soulMate.GetComponent<Creator>().ForceFinishTimer(true);
				isButtonUp = false;
			}
		}
	}
}
