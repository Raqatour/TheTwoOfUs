using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaOmega : MonoBehaviour
{
	public GameObject Orga;
	public GameObject Mecha;
	public bool touched = false;

	void Update()
	{
		if(Orga.GetComponent<Creator>().isEnded && Mecha.GetComponent<Creator>().isEnded)
		{
			touched = true;
		}
	}
}
