using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orgaRight : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga")
		{
			GetComponentInParent<TutLevel6>().orgaIsRight = true;
			GetComponentInParent<TutLevel6>().orgaIsNone = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga")
		{
			GetComponentInParent<TutLevel6>().orgaIsRight = false;
			GetComponentInParent<TutLevel6>().orgaIsNone = true;
		}
	}
}
