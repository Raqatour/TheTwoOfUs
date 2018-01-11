using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mechaGap : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Mecha")
		{
			GetComponentInParent<TutLevel4>().mechaGapped = true;
		}
	}
}
