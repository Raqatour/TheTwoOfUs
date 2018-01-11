using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutLevel5 : MonoBehaviour
{
	private bool orgaCanTry = true;
	private bool orgaHasTried = false;
	private bool mechaCanTry = true;
	private bool mechaHasTried = false;

	public bool orgaEntered = false;
	public bool mechaEntered = false;

	private float orgaTime = 5.0f;
	private float mechaTime = 5.0f;

	public Text orgaDont;
	public Text mechaDont;
	public Text orgaClock;
	public Text mechaClock;
	public Text orgaA;
	public Text mechaA;

	GamePadController.Controller gamePad1;
	GamePadController.Controller gamePad2;

	void Start()
	{
		gamePad1 = GamePadController.GamePadOne;
		gamePad2 = GamePadController.GamePadTwo;

		orgaDont.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		mechaDont.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		orgaA.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		mechaA.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
	}

	void Update()
	{
		if(gamePad1.RightTrigger == 1 && gamePad1.B.Held && orgaCanTry == true)
		{
			orgaHasTried = true;
			StartCoroutine(FadeInText(1f, orgaDont));
			orgaTime -= Time.deltaTime;
			if(orgaTime >= 5.0f)
			{
				orgaClock.text = " ";
			}
			if(orgaTime > 4f)
			{
				orgaClock.text = "5";
			}
			else if(orgaTime > 3f)
			{
				orgaClock.text = "4";
			}
			else if(orgaTime > 2f)
			{
				orgaClock.text = "3";
			}
			else if(orgaTime > 1f)
			{
				orgaClock.text = "2";
			}
			else if(orgaTime > 0f)
			{
				orgaClock.text = "1";
			}
			if(orgaTime <= 0)
			{
				orgaClock.text = " ";
			}
		}
		if(orgaHasTried == true && gamePad1.RightTrigger == 0)
		{
			StartCoroutine(FadeOutText(1f, orgaDont));
			orgaCanTry = false;
		}
		if(orgaCanTry == false)
		{
			orgaClock.text = " ";
		}

		if(gamePad2.RightTrigger == 1 && gamePad2.B.Held && mechaCanTry == true)
		{
			mechaHasTried = true;
			StartCoroutine(FadeInText(1f, mechaDont));
			mechaTime -= Time.deltaTime;
			if(mechaTime >= 5.0f)
			{
				mechaClock.text = " ";
			}
			if(mechaTime > 4f)
			{
				mechaClock.text = "5";
			}
			else if(mechaTime > 3f)
			{
				mechaClock.text = "4";
			}
			else if(mechaTime > 2f)
			{
				mechaClock.text = "3";
			}
			else if(mechaTime > 1f)
			{
				mechaClock.text = "2";
			}
			else if(mechaTime > 0f)
			{
				mechaClock.text = "1";
			}
			if(mechaTime <= 0)
			{
				mechaClock.text = " ";
			}
		}
		if(mechaHasTried == true && gamePad2.RightTrigger == 0)
		{
			StartCoroutine(FadeOutText(1f, mechaDont));
			mechaCanTry = false;
		}
		if(mechaCanTry == false)
		{
			mechaClock.text = " ";
		}

		if(orgaEntered == false || mechaEntered == false)
		{
			if(orgaCanTry == false && mechaCanTry == false)
			{
				StartCoroutine(FadeInText(1f, orgaA));
				StartCoroutine(FadeInText(1f, mechaA));
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
