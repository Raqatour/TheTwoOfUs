using System;
using TwoOfUs.Player;
using UnityEngine;

public class Reignite : MonoBehaviour
{
	bool isButtonUp;
	
	[SerializeField]
	protected new AudioSource audio;
	
	[SerializeField]
	protected AudioClip intake;

	private void Start()
	{
		audio = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		Creator otherCreator = other.GetComponent<Creator>();
		
		if(other.CompareTag("Orga"))
		{
			isButtonUp = true;
			otherCreator.SoulMate.ForceFinishTimer();
			otherCreator.IsGlowing = true;
			otherCreator.SoulMate.IsGlowing = true;
			otherCreator.IsIgnited = true;
			otherCreator.soulMate.GetComponent<Creator>().IsIgnited = true;
			if(!audio.isPlaying)
			{
				audio.PlayOneShot(intake, 1.0f);
			}
		}

		if (!other.CompareTag("Mecha"))
		{
			return;
		}

		isButtonUp = true;
		otherCreator.soulMate.GetComponent<Creator>().ForceFinishTimer();
		otherCreator.IsGlowing = true;
		otherCreator.SoulMate.IsGlowing = true;
		otherCreator.IsIgnited = true;
		otherCreator.soulMate.GetComponent<Creator>().IsIgnited = true;
		if(!audio.isPlaying)
		{
			audio.PlayOneShot(intake, 1.0f);
		}
	}

	void OnTriggerStay(Collider other)
	{
		Creator creator = other.GetComponent<Creator>();
		
		if(other.CompareTag("Orga"))
		{
			if(Math.Abs(creator.SoulMate.GamepadController.RightTrigger) < float.Epsilon && isButtonUp)
			{
				creator.ForceFinishTimer(true);
				creator.soulMate.GetComponent<Creator>().ForceFinishTimer(true);
				isButtonUp = false;
			}
		}

		if (!other.CompareTag("Mecha"))
		{
			return;
		}

		if (!(Math.Abs(creator.GamepadController.RightTrigger) < float.Epsilon) || !isButtonUp)
		{
			return;
		}

		creator.ForceFinishTimer(true);
		creator.SoulMate.ForceFinishTimer(true);
		isButtonUp = false;
	}
}
