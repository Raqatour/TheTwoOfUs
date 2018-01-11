using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
	public GamePadController.Controller gamePad1;
	public GamePadController.Controller gamePad2;

	private int count = 1;
	private bool canMove = true;

	void Start()
	{
		gamePad1 = GamePadController.GamePadOne;
		gamePad2 = GamePadController.GamePadTwo;
	}

	void Update()
	{
		if(count > 2)
		{
			count = 2;
		}

		if(count < 0)
		{
			count = 0;
		}

		if(canMove && gamePad1.LeftStick.X == 1)
		{
			count++;
			canMove = false;
		}

		if(canMove && gamePad2.LeftStick.X == -1)
		{
			count++;
			canMove = false;
		}

		if(canMove && gamePad1.LeftStick.X == -1)
		{
			count--;
			canMove = false;
		}

		if(canMove && gamePad2.LeftStick.X == 1)
		{
			count--;
			canMove = false;
		}

		if(count == 0)
		{
			transform.position = new Vector3(-125, -5, 0);
		}

		if(count == 1)
		{
			transform.position = new Vector3(20, -5, 0);
		}

		if(count == 2)
		{
			transform.position = new Vector3(165, -5, 0);
		}

		if(gamePad1.LeftStick.X == 0 && gamePad2.LeftStick.X == 0)
		{
			canMove = true;
		}

		if(gamePad1.A.Pressed || gamePad2.A.Pressed)
		{
			if(count == 0)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}

			if(count == 1)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
			}

			if(count == 2)
			{
				Application.Quit();
			}
		}
	}
}
