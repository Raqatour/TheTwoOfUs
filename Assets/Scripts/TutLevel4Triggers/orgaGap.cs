using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orgaGap : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga")
		{
			GetComponentInParent<TutLevel4>().orgaGapped = true;
		}
	}
}
