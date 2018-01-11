using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutLevel6 : MonoBehaviour
{
	public GamePadController.Controller gamePad1;
	public GamePadController.Controller gamePad2;

	public bool	orgaCanTry = true;
	public bool	mechaCanTry = true;
	public bool	orgaIn = false;
	public bool	mechaIn = false;
	public bool	orgaIsLeft = false;
	public bool	orgaIsRight = false;
	public bool	orgaIsNone = true;

	public GameObject orgaLeft;
	public GameObject orgaRight;
	public GameObject roomLeft;
	public GameObject roomRight;

	public Text	orgaStuck;
	public Text	mechaStuck;
	public Text	orgaLit;
	public Text	mechaLit;
	public Text orgaLeftIntense;
	public Text	mechaLeftIntense;
	public Text	orgaRightIntense;
	public Text	mechaRightIntense;

	private float timerlight = 0.0f;

	void Start()
	{
		gamePad1 = GamePadController.GamePadOne;
		gamePad2 = GamePadController.GamePadTwo;

		orgaStuck.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		mechaStuck.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		orgaLit.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		mechaLit.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		orgaLeftIntense.color = new Color(255, 255, 255, 0);
		mechaLeftIntense.color = new Color(255, 255, 255, 0);
		orgaRightIntense.color = new Color(255, 255, 255, 0);
		mechaRightIntense.color = new Color(255, 255, 255, 0);

		roomLeft.SetActive(false);
		roomRight.SetActive(false);
	}

	void Update()
	{
		if(gamePad1.RightTrigger == 1)
		{
			orgaCanTry = false;
		}
		if(gamePad2.RightTrigger == 1)
		{
			mechaCanTry = false;
		}

		if(orgaCanTry == false && mechaCanTry == false && orgaIn == true && mechaIn == true)
		{
			timerlight += Time.deltaTime;
			if(timerlight < 5f)
			{
				StartCoroutine(FadeInText(1f, orgaStuck));
				StartCoroutine(FadeInText(1f, mechaStuck));
			}
			else if(timerlight < 10f)
			{
				StartCoroutine(FadeOutText(1f, orgaStuck));
				StartCoroutine(FadeOutText(1f, mechaStuck));
				StartCoroutine(FadeInText(1f, orgaLit));
				StartCoroutine(FadeInText(1f, mechaLit));
			}
			else if(timerlight >= 10f)
			{
				timerlight = 10f;
				if(orgaIsRight == true || orgaIsNone == true)
				{
					roomLeft.SetActive(true);
					StartCoroutine(FadeInText(1f, orgaLeftIntense));
					StartCoroutine(FadeInText(1f, mechaLeftIntense));
					orgaLeft.SetActive(false);
					orgaRight.SetActive(false);
				}
				else if(orgaIsLeft == true)
				{
					roomRight.SetActive(true);
					StartCoroutine(FadeInText(1f, orgaRightIntense));
					StartCoroutine(FadeInText(1f, mechaRightIntense));
					orgaLeft.SetActive(false);
					orgaRight.SetActive(false);
				}
			}
		}
	}


	public IEnumerator FadeInText(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while(i.color.a < 1.0f)
		{
			{
				i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime/t));
				yield return null;
			}
		}
	}

	public IEnumerator FadeOutText(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while(i.color.a > 0.0f)
		{
			{
				i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime/t));
				yield return null;
			}
		}
	}
}
