using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
	private int end = 0;
	private int begin = 0;

	void Update()
	{
		if(begin == 1 && end == 1)
		{
			Debug.Log("Hit!");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Collider>().tag == "Mecha")
		{
			end = 1;
		}

		if(other.GetComponent<Collider>().tag == "Orga")
		{
			begin = 1;
		}
	}
}
