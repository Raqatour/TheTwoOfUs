using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutLevel4 : MonoBehaviour
{
	public GamePadController.Controller gamePad1;
	public GamePadController.Controller gamePad2;

	public GameObject orgaGap;
	public GameObject mechaGap;
	public GameObject orgaCome;
	public GameObject mechaCome;
	public GameObject insideTrig;

	public bool orgaGapped = false;
	public bool mechaGapped = false;
	public bool orgaCame = false;
	public bool mechaCame = false;
	public bool isInside = false;

	public Text instTouchOrga;
	public Text instTouchMecha;
	public Text orgaOhNo;
	public Text mechaOhNo;
	public Text orgaComeHere;
	public Text mechaComeHere;
	public Text orgaHRT;
	public Text mechaMoveTG;
	public Text mechaHRT;
	public Text orgaMoveTG;
	public Text poemOrga;
	public Text poemMecha;

	private float timerOhNo = 0.0f;

	void Start()
	{
		gamePad1 = GamePadController.GamePadOne;
		gamePad2 = GamePadController.GamePadTwo;

		instTouchOrga.color = new Color(231f/255f, 84f/255f, 128f/255f, 1);
		instTouchMecha.color = new Color(231f/255f, 84f/255f, 128f/255f, 1);
		orgaOhNo.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		mechaOhNo.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		orgaComeHere.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		mechaComeHere.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		orgaHRT.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		mechaMoveTG.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		mechaHRT.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		orgaMoveTG.color = new Color(231f/255f, 84f/255f, 128f/255f, 0);
		poemOrga.color = new Color(255f/255f, 255f/255f, 255f/255f, 0);
		poemMecha.color = new Color(255f/255f, 255f/255f, 255f/255f, 0);
	}

	void Update()
	{
		if(orgaGapped == true && mechaGapped == true)
		{
			StartCoroutine(FadeOutText(1f, instTouchOrga));
			StartCoroutine(FadeOutText(1f, instTouchMecha));
			if(instTouchOrga.color.a < 1f && instTouchMecha.color.a < 1f)
			{
				instTouchOrga.enabled = false;
				instTouchMecha.enabled = false;
				timerOhNo += Time.deltaTime;
				StartCoroutine(FadeInText(0.01f, orgaOhNo));
				StartCoroutine(FadeInText(0.01f, mechaOhNo));
				if(timerOhNo >= 5.0f)
				{
					StartCoroutine(FadeOutText(2f, orgaOhNo));
					StartCoroutine(FadeOutText(2f, mechaOhNo));
					timerOhNo = 5.0f;
				}
				if(orgaOhNo.color.a < 1f && mechaOhNo.color.a < 1f)
				{
					orgaOhNo.enabled = false;
					mechaOhNo.enabled = false;
					orgaCome.SetActive(true);
					mechaCome.SetActive(true);
					StartCoroutine(FadeInText(1f, orgaComeHere));
					StartCoroutine(FadeInText(1f, mechaComeHere));
				}
			}
		}

		if(mechaCame == true && orgaCame == true)
		{
			StartCoroutine(FadeOutText(1f, orgaComeHere));
			StartCoroutine(FadeOutText(1f, mechaComeHere));
			if(orgaComeHere.color.a < 1f && mechaComeHere.color.a < 1f)
			{
				StartCoroutine(FadeInText(0.001f, orgaHRT));
				orgaComeHere.enabled = false;
				mechaComeHere.enabled = false;
			}
		}

		if(gamePad1.RightTrigger == 1 && gamePad1.B.Held && orgaHRT.color.a >= 1f)
		{
			StartCoroutine(FadeInText(1f, mechaMoveTG));
		}

		if(isInside == true)
		{
			StartCoroutine(FadeOutText(1f, orgaHRT));
			StartCoroutine(FadeOutText(1f, mechaMoveTG));
			if(orgaHRT.color.a < 1f && mechaMoveTG.color.a < 1f)
			{
				StartCoroutine(FadeInText(0.001f, mechaHRT));
				orgaHRT.enabled = false;
				mechaMoveTG.enabled = false;
			}
		}

		if(gamePad2.RightTrigger == 1 && gamePad2.B.Held && mechaHRT.color.a >= 1f)
		{
			StartCoroutine(FadeInText(1f, orgaMoveTG));
		}

		if(GetComponentInParent<AlphaOmega>().touched)
		{
			StartCoroutine(FadeOutText(1f, mechaHRT));
			StartCoroutine(FadeOutText(1f, orgaMoveTG));
			if(mechaHRT.color.a < 1f && orgaMoveTG.color.a < 1f)
			{
				mechaHRT.enabled = false;
				orgaMoveTG.enabled = false;
				StartCoroutine(FadeInText(1f, poemOrga));
				StartCoroutine(FadeInText(1f, poemMecha));
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
