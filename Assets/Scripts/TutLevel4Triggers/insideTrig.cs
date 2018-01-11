using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insideTrig : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga" || other.GetComponent<Collider>().tag == "Mecha")
		{
			GetComponentInParent<TutLevel4>().isInside = true;
		}
	}
}
