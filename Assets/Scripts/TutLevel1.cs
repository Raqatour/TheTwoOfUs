using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutLevel1 : MonoBehaviour
{
	public Text instMoveJoyOrga;
	public Text instMoveJoyMecha;
	public Text instTouchOrga;
	public Text instTouchMecha;
	public Text poemOneOrga;
	public Text poemOneMecha;
	GamePadController.Controller gamePad1;
	GamePadController.Controller gamePad2;

	void Start()
	{
		gamePad1 = GamePadController.GamePadOne;
		gamePad2 = GamePadController.GamePadTwo;
		instMoveJoyOrga.color = new Color(231f/255f, 84f/255f, 128f/255f, 1);
		instMoveJoyMecha.color = new Color(231f/255f, 84f/255f, 128f/255f, 1);
		instTouchOrga.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		instTouchMecha.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		poemOneOrga.color = new Color(255, 255, 255, 0);
		poemOneMecha.color = new Color(255, 255, 255, 0);
	}

	void Update()
	{
		if(gamePad1.LeftStick.X > 0 || gamePad1.LeftStick.Y > 0)
		{
			StartCoroutine(FadeOutText(1f, instMoveJoyOrga));
		}
		if(gamePad2.LeftStick.X > 0 || gamePad2.LeftStick.Y > 0)
		{
			StartCoroutine(FadeOutText(1f, instMoveJoyMecha));
		}

		if(instMoveJoyOrga.color.a < 0.9f && instMoveJoyMecha.color.a < 0.1f)
		{
			instMoveJoyOrga.enabled = false;
			instMoveJoyMecha.enabled = false;
		}

		if(instMoveJoyOrga.enabled == false && instMoveJoyMecha.enabled == false && !GetComponentInParent<AlphaOmega>().touched)
		{
			StartCoroutine(FadeInText(1f, instTouchOrga));
			StartCoroutine(FadeInText(1f, instTouchMecha));
		}

		if(GetComponentInParent<AlphaOmega>().touched)
		{
			StartCoroutine(FadeOutText(1f, instTouchOrga));
			StartCoroutine(FadeOutText(1f, instTouchMecha));
			StartCoroutine(FadeInText(1f, poemOneOrga));
			StartCoroutine(FadeInText(1f, poemOneMecha));
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
