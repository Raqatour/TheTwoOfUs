﻿/*
	Attach yto "Origin" game object.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creator : MonoBehaviour
{
	public int particleCap;
	public GameObject particleTemplate;
	public int gender;
	public GameObject soulMate;
	
	// The Creator of the soul mate
	public Creator SoulMate { get; private set; }
	
	
	public float speed;
	public Rigidbody rb;
	public float radius;
	public int purgatory;
	public Vector3 pos;
	public float speedy = 10.0f;
	public GameObject bullet;
	public int ammo = 3;
	public int count = 0;
	public AudioClip exhale;
	public AudioClip inhale;
	public AudioClip whoosh;
	public AudioSource aud;
	public bool isOrgaGlowing = true;
	public bool isOrgaBig = false;
	public bool isMechaGlowing = true;
	public bool isMechaBig = false;
	public bool isIgnited = false;

	public int temp;

	private float horizontalZero;
	private float verticalZero;
	private Vector3 movementZero;
	private Vector3 wallDetection;
	private int setExternal = 0;
	private int countParticles = 0;
	private bool canShoot = true;
	private int track = 0;
	public bool isEnded = false;
	private bool isSpinning = false;

	public GameObject Ender;
	public GameObject heart;
	public bool m = false;
	public bool fire0 = false;
	public SphereCollider thisCollider;

	public float squeezeTime = 5.0f;
	public float timerSqueeze0 = 0.0f;
	public float timerSqueeze1 = 0.0f;

	public GamePadController.Controller gamePad1;
	public GamePadController.Controller gamePad2;

	public bool orgaInhaled = true;
	public bool mechaInhaled = true;

	private Light attachedLight;

	private void Awake()
	{
		attachedLight = GetComponent<Light>();
	}

	void Start()
	{
		gamePad1 = GamePadController.GamePadOne;
		gamePad2 = GamePadController.GamePadTwo;

		aud = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody>();


		if(this.gameObject.tag == "Orga")
		{
			soulMate = GameObject.FindGameObjectWithTag("Mecha");
			gender = 0;
		}
		else
		{
			soulMate = GameObject.FindGameObjectWithTag("Orga");
			gender = 1;
		}

		SoulMate = soulMate.GetComponent<Creator>();
		
		//Generates soul
		for(int i = 0; i < particleCap; i++)
		{
			GameObject childObject = Instantiate(particleTemplate, transform.position,
				Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), 0))) as GameObject;
			childObject.GetComponent<Orbit>().Init(this, attachedLight);
			childObject.transform.SetParent(transform);
		}

		heart = null;
	}

	void Update()
	{
		if(gamePad1.X.Pressed || gamePad2.X.Pressed)
		{
			SceneManager.LoadScene(0);	
		}

		if(timerSqueeze0 >= squeezeTime)
		{
			gamePad1.StopVibration();
		}
		else if(timerSqueeze0 >= 4*squeezeTime/5 && timerSqueeze0 < squeezeTime)
		{
			gamePad1.SetVibration(90, 90);
		}
		else if(timerSqueeze0 >= 3*squeezeTime/5 && timerSqueeze0 < 4*squeezeTime/5)
		{
			gamePad1.SetVibration(60, 60);
		}
		else if(timerSqueeze0 >= 2*squeezeTime/5 && timerSqueeze0 < 3*squeezeTime/5)
		{
			gamePad1.SetVibration(35, 35);
		}
		else if(timerSqueeze0 >= 1*squeezeTime/5 && timerSqueeze0 < 2*squeezeTime/5)
		{
			gamePad1.SetVibration(15, 15);
		}
		else if(timerSqueeze0 > 0 && timerSqueeze0 < squeezeTime/5)
		{
			gamePad1.SetVibration(5, 5);
		}

		if(gamePad1.RightTrigger == 0 && timerSqueeze0 > 0)
		{
			timerSqueeze0 = 5;
		}

		if(gamePad2.RightTrigger == 0 && timerSqueeze1 > 0)
		{
			timerSqueeze1 = 5;
		}

		if(timerSqueeze1 >= squeezeTime)
		{
			gamePad2.StopVibration();
		}
		else if(timerSqueeze1 >= 4*squeezeTime/5 && timerSqueeze1 < squeezeTime)
		{
			gamePad2.SetVibration(90, 90);
		}
		else if(timerSqueeze1 >= 3*squeezeTime/5 && timerSqueeze1 < 4*squeezeTime/5)
		{
			gamePad2.SetVibration(60, 60);
		}
		else if(timerSqueeze1 >= 2*squeezeTime/5 && timerSqueeze1 < 3*squeezeTime/5)
		{
			gamePad2.SetVibration(35, 35);
		}
		else if(timerSqueeze1 >= 1*squeezeTime/5 && timerSqueeze1 < 2*squeezeTime/5)
		{
			gamePad2.SetVibration(15, 15);
		}
		else if(timerSqueeze1 > 0 && timerSqueeze1 < squeezeTime/5)
		{
			gamePad2.SetVibration(5, 5);
		}

		if(gamePad1.RightTrigger == 0)
		{
			gamePad1.StopVibration();
		}

		if(gamePad2.RightTrigger == 0)
		{
			gamePad2.StopVibration();
		}
		
		if(gamePad1.A.Pressed || gamePad2.A.Pressed)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		//Movement and collider size change
		if(!isEnded)
		{
			if(this.gameObject.tag == "Orga")
			{
				if(ammo == 1)
				{
					SphereCollider myCollider = transform.GetComponent<SphereCollider>();
					myCollider.radius = 10;
					//thisCollider.radius = 10;
					//transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				}
				else if(ammo == 6)
				{
					SphereCollider myCollider = transform.GetComponent<SphereCollider>();
					myCollider.radius = 25;
					//thisCollider.radius = 25;
					//transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				}
				else if(ammo == 3)
				{
					SphereCollider myCollider = transform.GetComponent<SphereCollider>();
					myCollider.radius = 20;
					//thisCollider.radius = 20;
					//transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				}

				//rb.MovePosition(transform.position + transform.up * Time.deltaTime * speedy);
				rb.velocity = new Vector3(gamePad1.LeftStick.X * speedy, gamePad1.LeftStick.Y * speedy, 0);

				//Collider size change
				if(Input.GetKey(KeyCode.M) || gamePad1.RightTrigger == 1 && gamePad1.B.Held)
				{
					timerSqueeze0 += Time.deltaTime;
					if(isOrgaBig && timerSqueeze0 < squeezeTime)
					{
						soulMate.GetComponent<Creator>().ammo = 1;
						ammo = 6;
						if(!aud.isPlaying)
						{
							aud.PlayOneShot(exhale, 1.0f);
						}
					}
				}

				if(Input.GetKeyUp(KeyCode.M) || gamePad1.RightTrigger == 0 || timerSqueeze0 >= squeezeTime)
				{
					soulMate.GetComponent<Creator>().ammo = 3;
					ammo = 3;
				}

				if(timerSqueeze0 >= squeezeTime && orgaInhaled == true)
				{
					aud.Stop();
					if(!aud.isPlaying)
					{
						aud.PlayOneShot(inhale, 1.0f);
					}
					orgaInhaled = false;
				}
			}

			if(this.gameObject.tag == "Mecha")
			{
				if(ammo == 1)
				{
					SphereCollider myCollider = transform.GetComponent<SphereCollider>();
					myCollider.radius = 10;
					//thisCollider.radius = 10;
					//transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				}
				else if(ammo == 6)
				{
					SphereCollider myCollider = transform.GetComponent<SphereCollider>();
					myCollider.radius = 25;
					//thisCollider.radius = 25;
					//transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				}
				else if(ammo == 3)
				{
					SphereCollider myCollider = transform.GetComponent<SphereCollider>();
					myCollider.radius = 20;
					//thisCollider.radius = 20;
					//transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				}

				rb.velocity = new Vector3(gamePad2.LeftStick.X * -speedy, gamePad2.LeftStick.Y * -speedy, 0);

				if(Input.GetKey(KeyCode.V) || gamePad2.RightTrigger == 1 && gamePad2.B.Held)
				{
					timerSqueeze1 += Time.deltaTime;
					if(isMechaBig && timerSqueeze1 < squeezeTime)
					{
						soulMate.GetComponent<Creator>().ammo = 1;
						ammo = 6;
						if(!aud.isPlaying)
						{
							aud.PlayOneShot(exhale, 1.0f);
						}
					}
				}

				if(Input.GetKeyUp(KeyCode.V) || gamePad2.RightTrigger == 0 || timerSqueeze1 >= squeezeTime)
				{
					soulMate.GetComponent<Creator>().ammo = 3;
					ammo = 3;
				}

				if(timerSqueeze1 >= squeezeTime && mechaInhaled == true)
				{
					aud.Stop();
					if(!aud.isPlaying)
					{
						aud.PlayOneShot(inhale, 1.0f);
					}
					mechaInhaled = false;
				}
			}
		}

		if(isEnded)
		{
			isSpinning = true;
			soulMate.transform.localPosition = new Vector3(-20, 0, 0);
			transform.localPosition = new Vector3(20, 0, 0);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Mecha")
		{
			//if(isOrgaGlowing || soulMate.GetComponent<Creator>().isMechaGlowing)
			//{
			soulMate.GetComponent<SphereCollider>().enabled = false;
			this.GetComponent<SphereCollider>().enabled = false;
			isOrgaGlowing = true;
			soulMate.GetComponent<Creator>().isMechaGlowing = true;
			Instantiate(Ender, new Vector3((transform.position.x + other.transform.position.x)/2, (transform.position.y + other.transform.position.y)/2, 0), Quaternion.identity);
			heart = GameObject.FindWithTag("Heart");
			if(heart != null)
			{
				if(!aud.isPlaying)
				{
					aud.PlayOneShot(whoosh, 0.5f);
				}
				soulMate.transform.parent = heart.transform;
				this.transform.parent = heart.transform;
				isEnded = true;
				soulMate.GetComponent<Creator>().isEnded = true;
			}
			//}
		}

		/*if(other.collider.tag == "Wall")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}*/
	}
}