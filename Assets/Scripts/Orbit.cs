using System;
using TwoOfUs.Player;
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
		}
		ParentLight = GetComponentInParent<Light>();
	}

	public void UpdateOrbit ()
	{
		RenderingStates();
		
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
		var triggerrNotPressed = Creator.GamepadHelper.IsSquezzingReleased &&
		                         Creator.SoulMate.GamepadHelper.IsSquezzingReleased;
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

	private void SubGender()
	{
		bool squeezing = Creator.GamepadHelper.IsSqueezing;
			
		if (Input.GetKey(KeyCode.M) || squeezing && !Creator.IsIgnited)
		{
			// is squeeze timer still running
			if (Creator.IsSqueezeTimerRunning)
			{
				if (Creator.IsGlowing)
				{
					Creator.isBig = true;
				}
			}
		}
			
		if (Creator.isBig && Creator.GamepadHelper.IsSquezzingReleased )
		{
			Creator.IsGlowing = false;
			Creator.isBig = false;
		}
	}

	private void RenderingStates()
	{
		if (ParentLight == null)
		{
			ParentLight = transform.parent.GetComponent<Light>();
		}
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

		/*ParentLight.range = 75 / (Creator.SqueezeTimer != null) ? Creator.SqueezeTimer.Time : 0;
		ParentLight.color = Color32.Lerp(new Color32(0x69, 0x4F, 0xAC, 0x80),
			new Color32(0x4B, 0x00, 0x82, 0x80), Creator.SqueezeTimer.Time / 5);*/
	}
}
