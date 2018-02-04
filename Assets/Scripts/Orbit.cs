using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
	public float speed;
	public int maxRadius;
	public float orbitDistance;
	public float callbackDistance;
	public float minSpeed;
	public float maxSpeed;
	public int death;
	public float clipsize = 1.0f;
	public Material GrayMat;
	public Material GlowMat;

	public float radius;
	private int choose;
	private float posX = 0;
	private float posY = 0;
	private bool isIn = true;
	private int spinSpeed;
	private int spinDir;
	private Vector3 threeWay;
	private int radical; //Determines whether a particle will expand out during a respective squeeze
	private int subGender;
	private int tempGender;
	private float varRadius = 5.0f;


	public Creator Creator { get; set; }
	
	// Components
	[SerializeField]
	protected TrailRenderer trailRenderer;
	public Light ParentLight { get; set; }

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
			trailRenderer = GetComponent<TrailRenderer>();
			ParentLight = GetComponentInParent<Light>();
		}
		subGender = Creator.gender;

		//Controlled chaos in the particle behaviour
		spinSpeed = Random.Range(75, 100);
		spinDir = Random.Range(1, 3);
		radical = 1;//Random.Range(1, 2);

		//Randomises spin direction to one of the three axes
		if(spinDir == 1)
		{
			threeWay = new Vector3(1, 0, 0);
		}

		if(spinDir == 2)
		{
			threeWay = new Vector3(0, 1, 0);
		}

		if(spinDir == 3)
		{
			threeWay = new Vector3(0, 0, 1);
		}
	}

	public void UpdateOrbit ()
	{
		RenderingStates();
		SetData();
		AmmoState();
		SubGender();	
		DefaultOrbiting();
		WhilstTimerSqueezeRunning();
		OtherSqueezeTimer();
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
			Creator.isOrgaGlowing = true;
		}
		else
		{
			Creator.isMechaGlowing = true;
		}

		var visiblyActive = Creator.isOrgaGlowing  &&	Creator.isMechaGlowing  && Creator.IsIgnited;
		if (!visiblyActive)
		{
			return;
		}
		Creator.IsIgnited = false;
		Creator.isMechaGlowing = true;
		Creator.isOrgaGlowing = true;
	}

	private void OtherSqueezeTimer()
	{
		if (Creator.timerSqueeze1 >= Creator.squeezeTime ||
		    Creator.SoulMate.timerSqueeze1 >=
		    Creator.squeezeTime)
		{
			if (Creator.gamePad2.RightTrigger == 1 &&
			    Creator.gamePad2.B.Held)
			{
				radius = varRadius * orbitDistance; //clipsize;
				transform.Translate(Vector3.forward * Time.deltaTime * speed);
				transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

				//General pulsating
				if (Vector3.Distance(transform.parent.position, transform.position) > radius)
				{
					isIn = false;
				}

				if (isIn == false)
				{
					transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
					speed = Random.Range(minSpeed, maxSpeed);
					if (isIn == false)
					{
						choose = Random.Range(1, 4);

						if (choose == 1)
						{
							posX = Random.Range(-120, 250);
						}

						if (choose == 2)
						{
							posX = Random.Range(120, -250);
						}

						if (choose == 3)
						{
							posY = Random.Range(-120, 250);
						}

						if (choose == 4)
						{
							posY = Random.Range(120, -250);
						}
					}

					transform.eulerAngles = new Vector3(posX, posY, 0f);
				}

				if (Vector3.Distance(transform.parent.position, transform.position) < callbackDistance)
				{
					isIn = true;
					posX = 0f;
					posY = 0f;
				}
			}
		}
	}

	private void WhilstTimerSqueezeRunning()
	{
		if (Creator.timerSqueeze0 >= Creator.squeezeTime ||
		    Creator.SoulMate.timerSqueeze0 >=
		    Creator.squeezeTime)
		{
			if (Creator.gamePad1.RightTrigger == 1 &&
			    Creator.gamePad1.B.Held)
			{
				radius = varRadius * orbitDistance; //clipsize;
				transform.Translate(Vector3.forward * Time.deltaTime * speed);
				transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

				//General pulsating
				if (Vector3.Distance(transform.parent.position, transform.position) > radius)
				{
					isIn = false;
				}

				if (isIn == false)
				{
					transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
					speed = Random.Range(minSpeed, maxSpeed);
					if (isIn == false)
					{
						choose = Random.Range(1, 4);

						if (choose == 1)
						{
							posX = Random.Range(-120, 250);
						}

						if (choose == 2)
						{
							posX = Random.Range(120, -250);
						}

						if (choose == 3)
						{
							posY = Random.Range(-120, 250);
						}

						if (choose == 4)
						{
							posY = Random.Range(120, -250);
						}
					}

					transform.eulerAngles = new Vector3(posX, posY, 0f);
				}

				if (Vector3.Distance(transform.parent.position, transform.position) < callbackDistance)
				{
					isIn = true;
					posX = 0f;
					posY = 0f;
				}
			}
		}
	}

	private void DefaultOrbiting()
	{
		if (!Input.GetKey(KeyCode.V) && !Input.GetKey(KeyCode.M) &&
		    Creator.gamePad1.RightTrigger == 0 &&
		    Creator.gamePad2.RightTrigger == 0 || Creator.IsIgnited)
		{
			radius = varRadius * orbitDistance; //clipsize;
			transform.Translate(Vector3.forward * Time.deltaTime * speed);
			transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

			//General pulsating
			if (Vector3.Distance(transform.parent.position, transform.position) > radius)
			{
				isIn = false;
			}

			if (isIn == false)
			{
				transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
				speed = Random.Range(minSpeed, maxSpeed);
				if (isIn == false)
				{
					choose = Random.Range(1, 4);

					if (choose == 1)
					{
						posX = Random.Range(-120, 250);
					}

					if (choose == 2)
					{
						posX = Random.Range(120, -250);
					}

					if (choose == 3)
					{
						posY = Random.Range(-120, 250);
					}

					if (choose == 4)
					{
						posY = Random.Range(120, -250);
					}
				}

				transform.eulerAngles = new Vector3(posX, posY, 0f);
			}

			if (Vector3.Distance(transform.parent.position, transform.position) < callbackDistance)
			{
				isIn = true;
				posX = 0f;
				posY = 0f;
			}
		}
	}

	private void SetData()
	{
		transform.localScale =
			new Vector3(0.5f, 0.5f, 0.5f) * (2.0f - transform.parent.localScale.x);
		Creator.radius = radius;
		varRadius = transform.parent.childCount / 3000.0f;
		subGender = Creator.gender;
		death = Creator.purgatory;
	}

	private void SubGender()
	{
		if (subGender == 0)
		{
			//Orga
			//Spinning and orbiting
			if (Input.GetKey(KeyCode.M) || Creator.gamePad1.RightTrigger == 1 &&
			    Creator.gamePad1.B.Held && !Creator.IsIgnited)
			{
				if (radical == 1 && Creator.timerSqueeze0 <
				    Creator.squeezeTime)
				{
					if (Creator.isOrgaGlowing)
					{
						Creator.isOrgaBig = true;
						//moving, spinning, and expanding
						radius = varRadius *
						         maxRadius; 
					}

					transform.Translate(Vector3.forward * Time.deltaTime * speed);
					transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

					//General pulsating
					if (Vector3.Distance(transform.parent.position, transform.position) > radius)
					{
						isIn = false;
					}

					if (isIn == false)
					{
						transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
						speed = Random.Range(minSpeed, maxSpeed);
						if (isIn == false)
						{
							choose = Random.Range(1, 4);

							if (choose == 1)
							{
								posX = Random.Range(-120, 250);
							}

							if (choose == 2)
							{
								posX = Random.Range(120, -250);
							}

							if (choose == 3)
							{
								posY = Random.Range(-120, 250);
							}

							if (choose == 4)
							{
								posY = Random.Range(120, -250);
							}
						}

						transform.eulerAngles = new Vector3(posX, posY, 0f);
					}

					if (Vector3.Distance(transform.parent.position, transform.position) < callbackDistance)
					{
						isIn = true;
						posX = 0f;
						posY = 0f;
					}
				}
			}
			else if (Input.GetKey(KeyCode.V) || Creator.gamePad2.RightTrigger == 1 &&
			         Creator.gamePad2.B.Held && !Creator.IsIgnited)
			{
				if (Creator.SoulMate.isMechaGlowing &&
				    Creator.SoulMate.timerSqueeze1 <
				    Creator.squeezeTime)
				{
					//Mecha
					//moving, spinning, and expanding
					radius = varRadius * orbitDistance * 0.3f; //clipsize;
				}

				transform.Translate(Vector3.forward * Time.deltaTime * speed);
				transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

				//General pulsating
				if (Vector3.Distance(transform.parent.position, transform.position) > radius)
				{
					isIn = false;
				}

				if (isIn == false)
				{
					transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
					speed = Random.Range(minSpeed, maxSpeed);
					if (isIn == false)
					{
						choose = Random.Range(1, 4);

						if (choose == 1)
						{
							posX = Random.Range(-120, 250);
						}

						if (choose == 2)
						{
							posX = Random.Range(120, -250);
						}

						if (choose == 3)
						{
							posY = Random.Range(-120, 250);
						}

						if (choose == 4)
						{
							posY = Random.Range(120, -250);
						}
					}

					transform.eulerAngles = new Vector3(posX, posY, 0f);
				}

				if (Vector3.Distance(transform.parent.position, transform.position) < callbackDistance)
				{
					isIn = true;
					posX = 0f;
					posY = 0f;
				}
			}

			if (Creator.isOrgaBig && Creator.gamePad1.RightTrigger == 0)
			{
				Creator.isOrgaGlowing = false;
				Creator.isOrgaBig = false;
			}
		}
		else
		{
			//Spinning and orbiting
			if (Input.GetKey(KeyCode.V) || Creator.gamePad2.RightTrigger == 1 &&
			    Creator.gamePad2.B.Held && !Creator.IsIgnited)
			{
				if (radical == 1 && Creator.timerSqueeze1 <
				    Creator.squeezeTime)
				{
					if (Creator.isMechaGlowing)
					{
						Creator.isMechaBig = true;
						radius = varRadius *
						         maxRadius; //* Mathf.Sqrt((Input.GetAxis("Horizontal1")*Input.GetAxis("Horizontal1")) + (Input.GetAxis("Vertical1")*Input.GetAxis("Vertical1")));
					}

					transform.Translate(Vector3.forward * Time.deltaTime * speed);
					transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);
					if (Vector3.Distance(transform.parent.position, transform.position) > radius)
					{
						isIn = false;
					}

					if (isIn == false)
					{
						transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
						speed = Random.Range(minSpeed, maxSpeed);
						if (isIn == false)
						{
							choose = Random.Range(1, 4);

							if (choose == 1)
							{
								posX = Random.Range(-120, 250);
							}

							if (choose == 2)
							{
								posX = Random.Range(120, -250);
							}

							if (choose == 3)
							{
								posY = Random.Range(-120, 250);
							}

							if (choose == 4)
							{
								posY = Random.Range(120, -250);
							}
						}

						transform.eulerAngles = new Vector3(posX, posY, 0f);
					}

					if (Vector3.Distance(transform.parent.position, transform.position) < callbackDistance)
					{
						isIn = true;
						posX = 0f;
						posY = 0f;
					}
				}
			}
			else if (Input.GetKey(KeyCode.M) || Creator.gamePad1.RightTrigger == 1 &&
			         Creator.gamePad1.B.Held && !Creator.IsIgnited)
			{
				if (Creator.SoulMate.isOrgaGlowing &&
				    Creator.SoulMate.timerSqueeze0 <
				    Creator.squeezeTime)
				{
					radius = varRadius * orbitDistance * 0.3f; //clipsize;
				}

				transform.Translate(Vector3.forward * Time.deltaTime * speed);
				transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

				if (Vector3.Distance(transform.parent.position, transform.position) > radius)
				{
					isIn = false;
				}

				if (isIn == false)
				{
					transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
					speed = Random.Range(minSpeed, maxSpeed);
					if (isIn == false)
					{
						choose = Random.Range(1, 4);

						if (choose == 1)
						{
							posX = Random.Range(-120, 250);
						}

						if (choose == 2)
						{
							posX = Random.Range(120, -250);
						}

						if (choose == 3)
						{
							posY = Random.Range(-120, 250);
						}

						if (choose == 4)
						{
							posY = Random.Range(120, -250);
						}
					}

					transform.eulerAngles = new Vector3(posX, posY, 0f);
				}

				if (Vector3.Distance(transform.parent.position, transform.position) < callbackDistance)
				{
					isIn = true;
					posX = 0f;
					posY = 0f;
				}
			}

			if (Creator.isMechaBig && Creator.gamePad2.RightTrigger == 0)
			{
				Creator.isMechaGlowing = false;
				Creator.isMechaBig = false;
			}
		}
	}

	private void AmmoState()
	{
		switch (Creator.ammo)
		{
			case 6:
				clipsize = 2.0f;
				break;
			case 5:
				clipsize = 1.6f;
				break;
			case 4:
				clipsize = 1.3f;
				break;
			case 3:
				clipsize = 1.0f;
				break;
			case 2:
				clipsize = 0.6f;
				break;
			case 1:
				clipsize = 0.3f;
				break;
			default:
				clipsize = 0.1f;
				break;
		}
	}

	private void RenderingStates()
	{
		if (!Creator.isOrgaGlowing || !Creator.isMechaGlowing)
		{
			trailRenderer.material = GrayMat;
			ParentLight.intensity = 75;
			ParentLight.range = 75;
			ParentLight.color = new Color32(0x4B, 0x00, 0x82, 0x80);
		}
		else
		{
			trailRenderer.material = GlowMat;
			ParentLight.intensity = 75;
			ParentLight.range = 75;
			ParentLight.color = new Color32(0x69, 0x4F, 0xAC, 0x80);
		}

		if (Creator.isOrgaBig && !Creator.IsIgnited)
		{
			trailRenderer.material = GrayMat;
			ParentLight.range = 75 / Creator.timerSqueeze0;
			ParentLight.color = Color32.Lerp(new Color32(0x69, 0x4F, 0xAC, 0x80),
				new Color32(0x4B, 0x00, 0x82, 0x80), Creator.timerSqueeze0 / 5);
		}

		if (Creator.isMechaBig && !Creator.IsIgnited)
		{
			trailRenderer.material = GrayMat;
			ParentLight.range = 75 / Creator.timerSqueeze1;
			ParentLight.color = Color32.Lerp(new Color32(0x69, 0x4F, 0xAC, 0x80),
				new Color32(0x4B, 0x00, 0x82, 0x80), Creator.timerSqueeze1 / 5);
		}
	}
}
