using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
	public float speed;
	public int maxRadius;
	public float oRange;
	public float flirtTime;
	public float orbitDistance;
	public float callbackDistance;
	public float minSpeed;
	public float maxSpeed;
	public int death = 1;
	public GameObject[] balls1;
	public GameObject[] balls2;
	public bool isExternal;
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
	private GameObject uBuntu;
	private int tempGender;
	private float varRadius = 5.0f;

	public int temp;

	void Start()
	{
		uBuntu = transform.parent.gameObject;

		//Each particle has the gender of its parent
		subGender = uBuntu.GetComponent<Creator>().gender;

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

	void Update ()
	{

		if(!GetComponentInParent<Creator>().isOrgaGlowing || !GetComponentInParent<Creator>().isMechaGlowing)
		{
			this.GetComponent<TrailRenderer>().material = GrayMat;
			GetComponentInParent<Light>().intensity = 75;
			GetComponentInParent<Light>().range = 75;
			GetComponentInParent<Light>().color = new Color32(0x4B, 0x00, 0x82, 0x80);
		}
		else
		{
			this.GetComponent<TrailRenderer>().material = GlowMat;
			GetComponentInParent<Light>().intensity = 75;
			GetComponentInParent<Light>().range = 75;
			GetComponentInParent<Light>().color = new Color32(0x69, 0x4F, 0xAC, 0x80);
		}

		if(GetComponentInParent<Creator>().isOrgaBig && !GetComponentInParent<Creator>().isIgnited)
		{
			this.GetComponent<TrailRenderer>().material = GrayMat;
			GetComponentInParent<Light>().range = 75/GetComponentInParent<Creator>().timerSqueeze0;
			GetComponentInParent<Light>().color = Color32.Lerp(new Color32(0x69, 0x4F, 0xAC, 0x80), new Color32(0x4B, 0x00, 0x82, 0x80), GetComponentInParent<Creator>().timerSqueeze0/5);
		}

		if(GetComponentInParent<Creator>().isMechaBig && !GetComponentInParent<Creator>().isIgnited)
		{
			this.GetComponent<TrailRenderer>().material = GrayMat;
			GetComponentInParent<Light>().range = 75/GetComponentInParent<Creator>().timerSqueeze1;
			GetComponentInParent<Light>().color = Color32.Lerp(new Color32(0x69, 0x4F, 0xAC, 0x80), new Color32(0x4B, 0x00, 0x82, 0x80), GetComponentInParent<Creator>().timerSqueeze1/5);
		}

		/*if(GetComponentInParent<Creator>().isMechaBig && GetComponentInParent<Creator>().isIgnited)
		{
			this.GetComponent<TrailRenderer>().material = GlowMat;
			GetComponentInParent<Light>().intensity = 75;
			GetComponentInParent<Light>().range = 75;
		}

		if(GetComponentInParent<Creator>().isOrgaBig && GetComponentInParent<Creator>().isIgnited)
		{
			this.GetComponent<TrailRenderer>().material = GlowMat;
			GetComponentInParent<Light>().intensity = 75;
			GetComponentInParent<Light>().range = 50;
		}*/
		
		temp = transform.parent.childCount;
		transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) * (2.0f - transform.parent.GetComponent<Transform>().localScale.x);
		transform.parent.GetComponent<Creator>().radius = radius;
		varRadius = transform.parent.childCount/3000.0f;
		subGender = transform.parent.GetComponent<Creator>().gender;
		death = transform.parent.GetComponent<Creator>().purgatory;

		switch(GetComponentInParent<Creator>().ammo)
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

			if(subGender == 0)
			{
				//Orga
				//Spinning and orbiting
				if(Input.GetKey(KeyCode.M) || GetComponentInParent<Creator>().gamePad1.RightTrigger == 1 && GetComponentInParent<Creator>().gamePad1.B.Held && !GetComponentInParent<Creator>().isIgnited)
				{
					if(radical == 1 && GetComponentInParent<Creator>().timerSqueeze0 < GetComponentInParent<Creator>().squeezeTime)
					{
						if(GetComponentInParent<Creator>().isOrgaGlowing)
						{
							GetComponentInParent<Creator>().isOrgaBig = true;
							//moving, spinning, and expanding
							radius = varRadius * maxRadius; //* Mathf.Sqrt((Input.GetAxis("Horizontal0")*Input.GetAxis("Horizontal0")) + (Input.GetAxis("Vertical0")*Input.GetAxis("Vertical0")));
						}
						transform.Translate (Vector3.forward * Time.deltaTime * speed);
						transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

						//General pulsating
						if (Vector3.Distance (transform.parent.position, transform.position) > radius)
						{
							isIn = false;
						}
						if(isIn == false)
						{
							transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
							speed = Random.Range(minSpeed, maxSpeed);
							if (isIn == false)
							{
								choose = Random.Range (1, 4);

								if (choose == 1) {
									posX = Random.Range (-120, 250);
								}
								if (choose == 2) {
									posX = Random.Range (120, -250);
								}
								if (choose == 3) {
									posY = Random.Range (-120, 250);
								}
								if (choose == 4) {
									posY = Random.Range (120, -250);
								}
							}
							transform.eulerAngles = new Vector3(posX, posY, 0f);
						}
						if(Vector3.Distance (transform.parent.position, transform.position) < callbackDistance)
						{
							isIn = true;
							posX = 0f;
							posY = 0f;
						}
					}
				}
				else if(Input.GetKey(KeyCode.V) || GetComponentInParent<Creator>().gamePad2.RightTrigger == 1 && GetComponentInParent<Creator>().gamePad2.B.Held && !GetComponentInParent<Creator>().isIgnited)
				{
					if(GetComponentInParent<Creator>().soulMate.GetComponent<Creator>().isMechaGlowing && GetComponentInParent<Creator>().soulMate.GetComponent<Creator>().timerSqueeze1 < GetComponentInParent<Creator>().squeezeTime)
					{
						//Mecha
						//moving, spinning, and expanding
						radius = varRadius * orbitDistance * 0.3f;//clipsize;
					}

						transform.Translate (Vector3.forward * Time.deltaTime * speed);
						transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

						//General pulsating
						if (Vector3.Distance (transform.parent.position, transform.position) > radius)
						{
							isIn = false;
						}
						if(isIn == false)
						{
							transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
							speed = Random.Range(minSpeed, maxSpeed);
							if (isIn == false) {
								choose = Random.Range (1, 4);

								if (choose == 1) {
									posX = Random.Range (-120, 250);
								}
								if (choose == 2) {
									posX = Random.Range (120, -250);
								}
								if (choose == 3) {
									posY = Random.Range (-120, 250);
								}
								if (choose == 4) {
									posY = Random.Range (120, -250);
								}
							}
							transform.eulerAngles = new Vector3(posX, posY, 0f);
						}
						if(Vector3.Distance (transform.parent.position, transform.position) < callbackDistance)
						{
							isIn = true;
							posX = 0f;
							posY = 0f;
						}
				}
				if(GetComponentInParent<Creator>().isOrgaBig && GetComponentInParent<Creator>().gamePad1.RightTrigger == 0)
				{
					GetComponentInParent<Creator>().isOrgaGlowing = false;
					GetComponentInParent<Creator>().isOrgaBig = false;
				}
			}
	////////////////////////////////////////////////////////
			else
			{
				//Spinning and orbiting
				if(Input.GetKey(KeyCode.V) || GetComponentInParent<Creator>().gamePad2.RightTrigger == 1 && GetComponentInParent<Creator>().gamePad2.B.Held && !GetComponentInParent<Creator>().isIgnited)
				{
					if(radical == 1 && GetComponentInParent<Creator>().timerSqueeze1 < GetComponentInParent<Creator>().squeezeTime)
					{
						if(GetComponentInParent<Creator>().isMechaGlowing)
						{	
							GetComponentInParent<Creator>().isMechaBig = true;
							radius = varRadius * maxRadius; //* Mathf.Sqrt((Input.GetAxis("Horizontal1")*Input.GetAxis("Horizontal1")) + (Input.GetAxis("Vertical1")*Input.GetAxis("Vertical1")));
						}

						transform.Translate (Vector3.forward * Time.deltaTime * speed);
						transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);
						if (Vector3.Distance (transform.parent.position, transform.position) > radius)
						{
							isIn = false;
						}
						if(isIn == false)
						{
							transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
							speed = Random.Range(minSpeed, maxSpeed);
							if (isIn == false) {
								choose = Random.Range (1, 4);

								if (choose == 1) {
									posX = Random.Range (-120, 250);
								}
								if (choose == 2) {
									posX = Random.Range (120, -250);
								}
								if (choose == 3) {
									posY = Random.Range (-120, 250);
								}
								if (choose == 4) {
									posY = Random.Range (120, -250);
								}
							}
							transform.eulerAngles = new Vector3(posX, posY, 0f);
						}
						if(Vector3.Distance (transform.parent.position, transform.position) < callbackDistance)
						{
							isIn = true;
							posX = 0f;
							posY = 0f;
						}
					}
				}
				else if(Input.GetKey(KeyCode.M) || GetComponentInParent<Creator>().gamePad1.RightTrigger == 1 && GetComponentInParent<Creator>().gamePad1.B.Held && !GetComponentInParent<Creator>().isIgnited)
				{
					if(GetComponentInParent<Creator>().soulMate.GetComponent<Creator>().isOrgaGlowing && GetComponentInParent<Creator>().soulMate.GetComponent<Creator>().timerSqueeze0 < GetComponentInParent<Creator>().squeezeTime)
					{
						radius = varRadius * orbitDistance * 0.3f;//clipsize;
					}

					transform.Translate (Vector3.forward * Time.deltaTime * speed);
					transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

					if (Vector3.Distance (transform.parent.position, transform.position) > radius)
					{
						isIn = false;
					}
					if(isIn == false)
					{
						transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
						speed = Random.Range(minSpeed, maxSpeed);
						if (isIn == false) {
							choose = Random.Range (1, 4);

							if (choose == 1) {
								posX = Random.Range (-120, 250);
							}
							if (choose == 2) {
								posX = Random.Range (120, -250);
							}
							if (choose == 3) {
								posY = Random.Range (-120, 250);
							}
							if (choose == 4) {
								posY = Random.Range (120, -250);
							}
						}
						transform.eulerAngles = new Vector3(posX, posY, 0f);
					}
					if(Vector3.Distance (transform.parent.position, transform.position) < callbackDistance)
					{
						isIn = true;
						posX = 0f;
						posY = 0f;
					}
				}

				if(GetComponentInParent<Creator>().isMechaBig && GetComponentInParent<Creator>().gamePad2.RightTrigger == 0)
				{
					GetComponentInParent<Creator>().isMechaGlowing = false;
					GetComponentInParent<Creator>().isMechaBig = false;
				}
			}	

		if(!Input.GetKey(KeyCode.V) && !Input.GetKey(KeyCode.M) && GetComponentInParent<Creator>().gamePad1.RightTrigger == 0 && GetComponentInParent<Creator>().gamePad2.RightTrigger == 0 || GetComponentInParent<Creator>().isIgnited)
		{
			radius = varRadius * orbitDistance;//clipsize;
			transform.Translate (Vector3.forward * Time.deltaTime * speed);
			transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

			//General pulsating
			if (Vector3.Distance (transform.parent.position, transform.position) > radius)
			{
				isIn = false;
			}
			if(isIn == false)
			{
				transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
				speed = Random.Range(minSpeed, maxSpeed);
				if (isIn == false) {
					choose = Random.Range (1, 4);

					if (choose == 1) {
						posX = Random.Range (-120, 250);
					}
					if (choose == 2) {
						posX = Random.Range (120, -250);
					}
					if (choose == 3) {
						posY = Random.Range (-120, 250);
					}
					if (choose == 4) {
						posY = Random.Range (120, -250);
					}
				}
				transform.eulerAngles = new Vector3(posX, posY, 0f);
			}
			if(Vector3.Distance (transform.parent.position, transform.position) < callbackDistance)
			{
				isIn = true;
				posX = 0f;
				posY = 0f;
			}
		}

		if(GetComponentInParent<Creator>().timerSqueeze0 >= GetComponentInParent<Creator>().squeezeTime || GetComponentInParent<Creator>().soulMate.GetComponent<Creator>().timerSqueeze0 >= GetComponentInParent<Creator>().squeezeTime)
		{
			if(GetComponentInParent<Creator>().gamePad1.RightTrigger == 1 && GetComponentInParent<Creator>().gamePad1.B.Held)
			{
				radius = varRadius * orbitDistance;//clipsize;
				transform.Translate (Vector3.forward * Time.deltaTime * speed);
				transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

				//General pulsating
				if (Vector3.Distance (transform.parent.position, transform.position) > radius)
				{
					isIn = false;
				}
				if(isIn == false)
				{
					transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
					speed = Random.Range(minSpeed, maxSpeed);
					if (isIn == false) {
						choose = Random.Range (1, 4);

						if (choose == 1) {
							posX = Random.Range (-120, 250);
						}
						if (choose == 2) {
							posX = Random.Range (120, -250);
						}
						if (choose == 3) {
							posY = Random.Range (-120, 250);
						}
						if (choose == 4) {
							posY = Random.Range (120, -250);
						}
					}
					transform.eulerAngles = new Vector3(posX, posY, 0f);
				}
				if(Vector3.Distance (transform.parent.position, transform.position) < callbackDistance)
				{
					isIn = true;
					posX = 0f;
					posY = 0f;
				}
			}
		}

		if(GetComponentInParent<Creator>().timerSqueeze1 >= GetComponentInParent<Creator>().squeezeTime ||  GetComponentInParent<Creator>().soulMate.GetComponent<Creator>().timerSqueeze1 >= GetComponentInParent<Creator>().squeezeTime)
		{
			if(GetComponentInParent<Creator>().gamePad2.RightTrigger == 1 && GetComponentInParent<Creator>().gamePad2.B.Held)
			{
				radius = varRadius * orbitDistance;//clipsize;
				transform.Translate (Vector3.forward * Time.deltaTime * speed);
				transform.RotateAround(transform.parent.position, threeWay, spinSpeed * Time.deltaTime);

				//General pulsating
				if (Vector3.Distance (transform.parent.position, transform.position) > radius)
				{
					isIn = false;
				}
				if(isIn == false)
				{
					transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, 2.5f);
					speed = Random.Range(minSpeed, maxSpeed);
					if (isIn == false) {
						choose = Random.Range (1, 4);

						if (choose == 1) {
							posX = Random.Range (-120, 250);
						}
						if (choose == 2) {
							posX = Random.Range (120, -250);
						}
						if (choose == 3) {
							posY = Random.Range (-120, 250);
						}
						if (choose == 4) {
							posY = Random.Range (120, -250);
						}
					}
					transform.eulerAngles = new Vector3(posX, posY, 0f);
				}
				if(Vector3.Distance (transform.parent.position, transform.position) < callbackDistance)
				{
					isIn = true;
					posX = 0f;
					posY = 0f;
				}
			}
		}

		if(GetComponentInParent<Creator>().isIgnited)
		{
			if(!Input.GetKey(KeyCode.V) && !Input.GetKey(KeyCode.M) && GetComponentInParent<Creator>().gamePad1.RightTrigger == 0 && GetComponentInParent<Creator>().gamePad2.RightTrigger == 0)
			{
				if(subGender == 0)
				{
					GetComponentInParent<Creator>().isOrgaGlowing = true;
				}
				else
				{
					GetComponentInParent<Creator>().isMechaGlowing = true;
				}

				if(GetComponentInParent<Creator>().isOrgaGlowing == true && GetComponentInParent<Creator>().isMechaGlowing == true && GetComponentInParent<Creator>().isIgnited == true)
				{
					GetComponentInParent<Creator>().isIgnited = false;
					GetComponentInParent<Creator>().isMechaGlowing = true;
					GetComponentInParent<Creator>().isOrgaGlowing = true;
				}
			}
		}
	}
}
