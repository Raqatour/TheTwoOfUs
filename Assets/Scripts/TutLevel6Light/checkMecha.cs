using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkMecha : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Mecha")
		{
			GetComponentInParent<TutLevel6>().mechaIn = true;
		}
	}
}
