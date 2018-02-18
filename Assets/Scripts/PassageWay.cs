using System.Collections;
using System.Collections.Generic;
using TwoOfUs.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassageWay : MonoBehaviour
{
	void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("Orga") || other.CompareTag("Mecha"))
		{
			Creator creator = other.GetComponent<Creator>();
			if(creator.TotalScale > 15)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}
}
