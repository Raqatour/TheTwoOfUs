using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutLevel2 : MonoBehaviour
{
	public Text tutUp;
	public Text tutDown;
	public Text poem1Up;
	public Text poem1Down;
	public Text poem2Up;
	public Text poem2Down;
	public Text clock0;
	public Text clock1;
	public Text Correction0;
	public Text Correction1;
	GamePadController.Controller gamePad1;
	GamePadController.Controller gamePad2;
	public float fadeTime;
	public float tickTime = 0.0f;

	private bool startFading = false;
	private bool orgaCanAttempt = true;
	private bool mechaCanAttempt = true;
	private bool orgaHasAttempted = false;
	private bool mechaHasAttempted = false;
	private float timerTick = 5.0f;

	void Start()
	{
		gamePad1 = GamePadController.GamePadOne;
		gamePad2 = GamePadController.GamePadTwo;
		poem1Up.color = new Color(255, 255, 255, 0);
		poem1Down.color = new Color(255, 255, 255, 0);
		poem2Up.color = new Color(255, 255, 255, 0);
		poem2Down.color = new Color(255, 255, 255, 0);
	}

	void Update()
	{
		if(gamePad1.RightTrigger == 0 && gamePad2.RightTrigger == 0 && !orgaCanAttempt && !mechaCanAttempt)
		{
			orgaHasAttempted = true;
			mechaHasAttempted = true;
		}

		if(gamePad1.RightTrigger > 0 || gamePad2.RightTrigger > 0)
		{
			startFading = true;
		}

		if(gamePad1.RightTrigger == 0 && gamePad2.RightTrigger == 0)
		{
			timerTick = 5f;
		}

		if(orgaCanAttempt || mechaCanAttempt)
		{
			if(startFading)
			{
				StartCoroutine(FadeInText(fadeTime, poem1Up));
				StartCoroutine(FadeInText(fadeTime, poem1Down));
				StartCoroutine(FadeInText(fadeTime, poem2Up));
				StartCoroutine(FadeInText(fadeTime, poem2Down));
				StartCoroutine(FadeOutText(fadeTime, tutUp));
				StartCoroutine(FadeOutText(fadeTime, tutDown));
			}

			if(timerTick == 5.0f)
			{
				clock0.text = " ";
				clock1.text = " ";
			}

			if(gamePad1.RightTrigger > 0 && orgaCanAttempt)
			{
				if(timerTick >= tickTime)
				{
					timerTick -= Time.deltaTime;
					if(timerTick > 4)
					{
						clock0.text = "5";
						clock1.text = "5";
					}
					else if(timerTick > 3)
					{
						clock0.text = "4";
						clock1.text = "4";
					}
					else if(timerTick > 2)
					{
						clock0.text = "3";
						clock1.text = "3";
					}
					else if(timerTick > 1)
					{
						clock0.text = "2";
						clock1.text = "2";
					}
					else if(timerTick > 0)
					{
						clock0.text = "1";
						clock1.text = "1";
					}

					if(timerTick <= 0)
					{
						clock0.text = " ";
						clock1.text = " ";
						orgaCanAttempt = false;
					}
				}	
			}

			if(gamePad2.RightTrigger > 0 && mechaCanAttempt)
			{
				if(timerTick >= tickTime)
				{
					timerTick -= Time.deltaTime;
					if(timerTick > 4)
					{
						clock0.text = "5";
						clock1.text = "5";
					}
					else if(timerTick > 3)
					{
						clock0.text = "4";
						clock1.text = "4";
					}
					else if(timerTick > 2)
					{
						clock0.text = "3";
						clock1.text = "3";
					}
					else if(timerTick > 1)
					{
						clock0.text = "2";
						clock1.text = "2";
					}
					else if(timerTick > 0)
					{
						clock0.text = "1";
						clock1.text = "1";
					}

					if(timerTick <= 0)
					{
						clock0.text = " ";
						clock1.text = " ";
						mechaCanAttempt = false;
					}
				}	
			}
		}
		else if(orgaHasAttempted && mechaHasAttempted)
		{
			StartCoroutine(FadeInText(2f, Correction0));
			StartCoroutine(FadeInText(2f, Correction1));
			StartCoroutine(FadeOutText(fadeTime, poem1Up));
			StartCoroutine(FadeOutText(fadeTime, poem1Down));
			StartCoroutine(FadeOutText(fadeTime, poem2Up));
			StartCoroutine(FadeOutText(fadeTime, poem2Down));
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
