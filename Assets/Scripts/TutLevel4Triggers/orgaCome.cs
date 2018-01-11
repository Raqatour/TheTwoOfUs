using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orgaCome : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga")
		{
			GetComponentInParent<TutLevel4>().orgaCame = true;
		}
	}
}
