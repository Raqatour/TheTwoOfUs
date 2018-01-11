using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class TestArduino : MonoBehaviour
{
	/*SerialPort sp = new SerialPort("COM4", 9600);

	// Use this for initialization
	void Start ()
	{
		sp.Open();
		sp.ReadTimeout = 2; //Unity freezes if too high
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(sp.IsOpen)
		{
			try
			{
				MoveObject(sp.ReadByte());
				print(sp.ReadByte());
			}
			catch (System.Exception)
			{
				
			}
		}
	}

	void MoveObject(int selButton)
	{
		if(selButton == 0)
		{
			this.gameObject.GetComponent<Creator>().blow0 = true;
		}

		if(selButton == 1)
		{
			this.gameObject.GetComponent<Creator>().blow0 = false;
		}

		if(selButton == 2)
		{
			this.gameObject.GetComponent<Creator>().moveUp = true;
		}
		else
		{
			this.gameObject.GetComponent<Creator>().moveUp = false;
		}

		if(selButton == 3)
		{
			this.gameObject.GetComponent<Creator>().moveDown = true;
		}
		else
		{
			this.gameObject.GetComponent<Creator>().moveDown = false;
		}

		if(selButton == 4)
		{
			this.gameObject.GetComponent<Creator>().moveLeft = true;
		}
		else
		{
			this.gameObject.GetComponent<Creator>().moveLeft = false;
		}

		if(selButton == 5)
		{
			this.gameObject.GetComponent<Creator>().moveRight = true;
		}
		else
		{
			this.gameObject.GetComponent<Creator>().moveRight = false;
		}
	}*/
}
