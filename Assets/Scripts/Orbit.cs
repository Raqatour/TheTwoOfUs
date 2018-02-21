using System.Collections;
using System.Collections.Generic;
using TwoOfUs.Player;
using TwoOfUs.Player.Characters;
using UnityEngine;

public class Orbit : MonoBehaviour
{
	private int subGender;
	public Creator Creator { get; set; }
	
	public Light ParentLight { get; set; }

	public float Scale
	{
		get { return transform.localScale.x; }
	}

	public void Init(Creator creator, Light light)
	{
		Creator = creator;
		ParentLight = light;
	}

	void Start()
	{
		//Each particle has the gender of its parent
		if (Creator == null)
		{
			Creator = GetComponentInParent<Creator>();
			ParentLight = GetComponentInParent<Light>();
		}
		subGender = Creator.Gender;
	}

	public void UpdateOrbit ()
	{
		RenderingStates();
		
		SetData();
		
		SubGender();	
		
		Ignited();
	}

	private void Ignited()
	{
		if (!Creator.IsIgnited)
		{
			return;
		}

		var vAndMnotPressed = !Input.GetKey(KeyCode.V) && !Input.GetKey(KeyCode.M);
		var triggerrNotPressed = Creator.gamePad1.RightTrigger == 0 & Creator.gamePad2.RightTrigger == 0;
		if (!vAndMnotPressed || !triggerrNotPressed)
		{
			return;
		}

		Creator.IsGlowing = true;

		var visiblyActive = Creator.IsGlowing  && Creator.IsIgnited;
		if (!visiblyActive)
		{
			return;
		}
		Creator.IsIgnited = false;
		Creator.IsGlowing = true;
		Creator.IsGlowing = true;
	}


	private void SetData()
	{
		subGender = Creator.Gender;
	}

	private void SubGender()
	{
		if (subGender == 0)
		{
			Orga orga = (Orga) Creator;				
			//Orga
			//Spinning and orbiting
			if (Input.GetKey(KeyCode.M) || Creator.gamePad1.RightTrigger == 1 && Creator.gamePad1.B.Held && !Creator.IsIgnited)
			{
				// is squeeze timer still running
				if (orga.SqueezeTimer.IsRunning)
				{
					if (orga.IsOrgaGlowing)
					{
						orga.isOrgaBig = true;
					}
				}
			}
			
			if (orga.isOrgaBig && Creator.gamePad1.RightTrigger == 0)
			{
				orga.IsOrgaGlowing = false;
				orga.isOrgaBig = false;
			}
		}
		else
		{
			Mecha mecha = (Mecha) Creator;
			
			//Spinning and orbiting
			if (Input.GetKey(KeyCode.V) || mecha.gamePad2.RightTrigger == 1 &&
			    mecha.gamePad2.B.Held && !mecha.IsIgnited)
			{
				if (mecha.SqueezeTimer.IsRunning)
				{
					if (mecha.IsMechaGlowing)
					{
						mecha.isMechaBig = true;
					}
				}
			}

			if (mecha.isMechaBig && Creator.gamePad2.RightTrigger == 0)
			{
				mecha.IsMechaGlowing = false;
				mecha.isMechaBig = false;
			}
		}
	}

	private void RenderingStates()
	{
		if (!Creator.IsGlowing)
		{
			ParentLight.intensity = 75;
			ParentLight.range = 75;
			ParentLight.color = new Color32(0x4B, 0x00, 0x82, 0x80);
		}
		else
		{
			ParentLight.intensity = 75;
			ParentLight.range = 75;
			ParentLight.color = new Color32(0x69, 0x4F, 0xAC, 0x80);
		}

		ParentLight.range = 75 / Creator.SqueezeTimer.Time;
		ParentLight.color = Color32.Lerp(new Color32(0x69, 0x4F, 0xAC, 0x80),
			new Color32(0x4B, 0x00, 0x82, 0x80), Creator.SqueezeTimer.Time / 5);
	}
}
