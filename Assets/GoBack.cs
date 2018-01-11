using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
	public GamePadController.Controller gamePad1;
	public GamePadController.Controller gamePad2;

	void Start()
	{
		gamePad1 = GamePadController.GamePadOne;
		gamePad2 = GamePadController.GamePadTwo;
	}

	void Update()
	{
		if(gamePad1.A.Pressed || gamePad2.A.Pressed)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
	}
}
