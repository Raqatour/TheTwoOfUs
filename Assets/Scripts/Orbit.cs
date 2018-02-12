using System.Collections;
using System.Collections.Generic;
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
		subGender = Creator.gender;
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

		if (subGender == 0) 
		{
			Creator.IsOrgaGlowing = true;
		}
		else
		{
			Creator.IsMechaGlowing = true;
		}

		var visiblyActive = Creator.IsOrgaGlowing  &&	Creator.IsMechaGlowing  && Creator.IsIgnited;
		if (!visiblyActive)
		{
			return;
		}
		Creator.IsIgnited = false;
		Creator.IsMechaGlowing = true;
		Creator.IsOrgaGlowing = true;
	}


	private void SetData()
	{
		subGender = Creator.gender;
	}

	private void SubGender()
	{
		if (subGender == 0)
		{
			//Orga
			//Spinning and orbiting
			if (Input.GetKey(KeyCode.M) || Creator.gamePad1.RightTrigger == 1 && Creator.gamePad1.B.Held && !Creator.IsIgnited)
			{
				if (Creator.timerSqueeze0 < Creator.squeezeTime)
				{
					if (Creator.IsOrgaGlowing)
					{
						Creator.isOrgaBig = true;
					}
				}
			}
			
			if (Creator.isOrgaBig && Creator.gamePad1.RightTrigger == 0)
			{
				Creator.IsOrgaGlowing = false;
				Creator.isOrgaBig = false;
			}
		}
		else
		{
			//Spinning and orbiting
			if (Input.GetKey(KeyCode.V) || Creator.gamePad2.RightTrigger == 1 &&
			    Creator.gamePad2.B.Held && !Creator.IsIgnited)
			{
				if (Creator.timerSqueeze1 < Creator.squeezeTime)
				{
					if (Creator.IsMechaGlowing)
					{
						Creator.isMechaBig = true;
					}
				}
			}

			if (Creator.isMechaBig && Creator.gamePad2.RightTrigger == 0)
			{
				Creator.IsMechaGlowing = false;
				Creator.isMechaBig = false;
			}
		}
	}

	private void RenderingStates()
	{
		if (!Creator.IsOrgaGlowing || !Creator.IsMechaGlowing)
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

		if (Creator.isOrgaBig && !Creator.IsIgnited)
		{
			ParentLight.range = 75 / Creator.timerSqueeze0;
			ParentLight.color = Color32.Lerp(new Color32(0x69, 0x4F, 0xAC, 0x80),
				new Color32(0x4B, 0x00, 0x82, 0x80), Creator.timerSqueeze0 / 5);
		}

		if (Creator.isMechaBig && !Creator.IsIgnited)
		{
			ParentLight.range = 75 / Creator.timerSqueeze1;
			ParentLight.color = Color32.Lerp(new Color32(0x69, 0x4F, 0xAC, 0x80),
				new Color32(0x4B, 0x00, 0x82, 0x80), Creator.timerSqueeze1 / 5);
		}
	}
}
