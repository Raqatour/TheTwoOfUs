using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutLevel1_1 : MonoBehaviour
{
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
		//instTouchOrga.color = new Color(91, 33, 50, 255);
		//instTouchMecha.color = new Color(91, 33, 50, 255);
		poemOneOrga.color = new Color(255, 255, 255, 0);
		poemOneMecha.color = new Color(255, 255, 255, 0);
	}

	void Update()
	{
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
