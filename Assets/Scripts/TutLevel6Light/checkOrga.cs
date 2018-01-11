using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkOrga : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga")
		{
			GetComponentInParent<TutLevel6>().orgaIn = true;
		}
	}
}
