using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassageWay : MonoBehaviour
{
	void OnTriggerStay(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Orga" || other.GetComponent<Collider>().tag == "Mecha")
		{
			if(other.GetComponent<SphereCollider>().radius > 15)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}
}
