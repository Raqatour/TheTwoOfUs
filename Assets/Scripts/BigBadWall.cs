using System.Collections;
using System.Collections.Generic;
using TwoOfUs.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigBadWall : MonoBehaviour
{
	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Orga" || other.collider.tag == "Mecha")
		{
			if(other.gameObject.GetComponent<Creator>().ammo == 6)
			{
				GetComponent<BoxCollider>().enabled = false;
			}

			if(other.gameObject.GetComponent<Creator>().ammo < 3)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}
}
