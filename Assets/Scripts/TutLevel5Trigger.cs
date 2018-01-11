using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutLevel5Trigger : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga")
		{
			GetComponentInParent<TutLevel5>().orgaEntered = true;
		}

		if(other.GetComponent<Collider>().tag == "Mecha")
		{
			GetComponentInParent<TutLevel5>().mechaEntered = true;
		}
	}
}
