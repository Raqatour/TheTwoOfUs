using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
	void OnCollisionStay(Collision other)
	{
		Debug.Log("hit");
	}
}
