using System.Collections;
using System.Collections.Generic;
using TwoOfUs.Player;
using UnityEngine;

public class Valve : MonoBehaviour
{
	public bool isEntered = false;

	void OnTriggerStay(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga" || other.GetComponent<Collider>().tag == "Mecha")
		{
			isEntered = true;
			other.GetComponent<Creator>().speedy = 100.0f;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga" || other.GetComponent<Collider>().tag == "Mecha")
		{
			isEntered = false;
			other.GetComponent<Creator>().speedy = 50.0f;
		}
	}
}
