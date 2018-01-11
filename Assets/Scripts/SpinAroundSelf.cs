using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpinAroundSelf : MonoBehaviour
{
	public float maxSpinSpeed;

	private float spinSpeed = 0.0f;

	void Update ()
	{
		if(spinSpeed < maxSpinSpeed)
		{
			spinSpeed += 5f;
		}
		else
		{
			spinSpeed = maxSpinSpeed;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		transform.RotateAround(transform.position, Vector3.forward, spinSpeed * Time.deltaTime);
	}
}
