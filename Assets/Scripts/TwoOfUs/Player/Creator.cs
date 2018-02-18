using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoOfUs.Player
{
	public class Creator : MonoBehaviour
	{	
		public int gender;
		public GameObject soulMate;
	
		// The Creator of the soul mate
		private Creator SoulMate { get; set; }
	
	
		public Rigidbody Rigidbody { get; private set; }
		public float speedy = 10.0f;
		public int ammo = 3;
	
		public AudioClip exhale;
		public AudioClip inhale;
		public AudioClip whoosh;
		public AudioSource aud;
	
		[SerializeField]
		protected bool isOrgaGlowing = true;
	
		[SerializeField]
		protected bool isMechaGlowing = true;

		public bool isOrgaBig;
		public bool isMechaBig;
		private bool isIgnited;


		public event Action<bool> OrgaGlowingChanged;
		public bool IsOrgaGlowing
		{
			get { return isOrgaGlowing; }
			set
			{
				if (OrgaGlowingChanged != null)
				{
					OrgaGlowingChanged(value);
				}
				isOrgaGlowing = value;
			}
		}

		public event Action<bool> MechaGlowingChanged;
		public bool IsMechaGlowing
		{
			get { return isMechaGlowing; }
			set
			{
				if  ( MechaGlowingChanged != null)
				{
					MechaGlowingChanged(value);
				}
				isMechaGlowing = value;
			}
		}

		// Ignited data
		public event Action<bool> IgnitedChanged;
		public bool IsIgnited
		{
			get { return isIgnited; }
			set
			{
				if (value != isIgnited)
				{
					isIgnited = value;
					if (IgnitedChanged != null)
					{
						IgnitedChanged(value);
					}
				}
			}
		}

		private float horizontalZero;
		private float verticalZero;
		private Vector3 movementZero;
		private Vector3 wallDetection;
		private int countParticles = 0;
		private bool canShoot = true;
		private int track = 0;
		public bool isEnded;
		private bool isSpinning;

		public GameObject Ender;
		public GameObject heart;

		public float squeezeTime = 5.0f;
		public float timerSqueeze0 = 0.0f;
		public float timerSqueeze1 = 0.0f;

		public GamePadController.Controller gamePad1;
		public GamePadController.Controller gamePad2;

		public bool orgaInhaled = true;
		public bool mechaInhaled = true;

		private Vector3 originalScale;

		public float Scale
		{
			get { return transform.localScale.x; }
		}

		public float TotalScale
		{
			get { return Scale * orbit.Scale; }
		}

		[SerializeField]
		protected float minimumSize = 0.5f;

		[SerializeField]
		protected float mediumSize = 1;
	
		[SerializeField]
		protected float maxSize = 1.25f;

		private Orbit orbit;

#if UNITY_EDITOR
		public KeyCode forceIgnite;
#endif

		protected virtual void Awake()
		{
			orbit = GetComponentInChildren<Orbit>();
		}

		void Start()
		{
			gamePad1 = GamePadController.GamePadOne;
			gamePad2 = GamePadController.GamePadTwo;

			originalScale = transform.localScale;
		
			aud = GetComponent<AudioSource>();
			Rigidbody = GetComponent<Rigidbody>();

			if(gameObject.CompareTag("Orga"))
			{
				soulMate = GameObject.FindGameObjectWithTag("Mecha");
			}
			else
			{
				soulMate = GameObject.FindGameObjectWithTag("Orga");
			}

			SoulMate = soulMate.GetComponent<Creator>();
		}

		protected virtual void Update()
		{
			orbit.UpdateOrbit();	
			// Update orbits
			GamePadCheck();
			//Movement and collider size change
			Movement();
#if UNITY_EDITOR
			if (Input.GetKeyDown(forceIgnite))
			{
				IsIgnited = !IsIgnited;
			}
#endif
		}

		private void Movement()
		{
			if (!isEnded)
			{
				OrgaMovement();

				MechaMovement();
			}
			else
			{
				if (isSpinning)
				{
					return;
				}
				isSpinning = true;
				IsMechaGlowing = IsOrgaGlowing = true;
				soulMate.transform.localPosition = new Vector3(-20, 0, 0);
				transform.localPosition = new Vector3(20, 0, 0);
			}
		}

		private void MechaMovement()
		{
			if (gameObject.CompareTag("Mecha"))
			{
				if (ammo == 1)
				{
					SetScale(minimumSize);
				}
				else if (ammo == 6)
				{
					SetScale(maxSize);
				}
				else if (ammo == 3)
				{
					SetScale(mediumSize);
				}

				Rigidbody.velocity = new Vector3(gamePad2.LeftStick.X * -speedy, gamePad2.LeftStick.Y * -speedy, 0);

				if (Input.GetKey(KeyCode.V) || gamePad2.RightTrigger == 1 && gamePad2.B.Held)
				{
					timerSqueeze1 += Time.deltaTime;
					if (isMechaBig && timerSqueeze1 < squeezeTime)
					{
						soulMate.GetComponent<Creator>().ammo = 1;
						ammo = 6;
						if (!aud.isPlaying)
						{
							aud.PlayOneShot(exhale, 1.0f);
						}
					}
				}

				if (Input.GetKeyUp(KeyCode.V) || gamePad2.RightTrigger == 0 || timerSqueeze1 >= squeezeTime)
				{
					soulMate.GetComponent<Creator>().ammo = 3;
					ammo = 3;
				}

				if (timerSqueeze1 >= squeezeTime && mechaInhaled == true)
				{
					aud.Stop();
					if (!aud.isPlaying)
					{
						aud.PlayOneShot(inhale, 1.0f);
					}

					mechaInhaled = false;
				}
			}
		}

		private void SetScale(float size)
		{
			transform.localScale = originalScale * size;
		}

		private void OrgaMovement()
		{
			if (gameObject.CompareTag("Orga"))
			{
				if (ammo == 1)
				{
					SetScale(minimumSize);
				}
				else if (ammo == 6)
				{
					SetScale(maxSize);
				}
				else if (ammo == 3)
				{
					SetScale(mediumSize);
				}

				Rigidbody.velocity = new Vector3(gamePad1.LeftStick.X * speedy, gamePad1.LeftStick.Y * speedy, 0);

				//Collider size change
				if (Input.GetKey(KeyCode.M) || gamePad1.RightTrigger == 1 && gamePad1.B.Held)
				{
					timerSqueeze0 += Time.deltaTime;
					if (isOrgaBig && timerSqueeze0 < squeezeTime)
					{
						soulMate.GetComponent<Creator>().ammo = 1;
						ammo = 6;
						if (!aud.isPlaying)
						{
							aud.PlayOneShot(exhale, 1.0f);
						}
					}
				}

				if (Input.GetKeyUp(KeyCode.M) || gamePad1.RightTrigger == 0 || timerSqueeze0 >= squeezeTime)
				{
					soulMate.GetComponent<Creator>().ammo = 3;
					ammo = 3;
				}

				if (timerSqueeze0 >= squeezeTime && orgaInhaled == true)
				{
					aud.Stop();
					if (!aud.isPlaying)
					{
						aud.PlayOneShot(inhale, 1.0f);
					}

					orgaInhaled = false;
				}
			}
		}

		private void GamePadCheck()
		{
			if (gamePad1.X.Pressed || gamePad2.X.Pressed)
			{
				SceneManager.LoadScene(0);
			}

			if (timerSqueeze0 >= squeezeTime)
			{
				gamePad1.StopVibration();
			}
			else if (timerSqueeze0 >= 4 * squeezeTime / 5 && timerSqueeze0 < squeezeTime)
			{
				gamePad1.SetVibration(90, 90);
			}
			else if (timerSqueeze0 >= 3 * squeezeTime / 5 && timerSqueeze0 < 4 * squeezeTime / 5)
			{
				gamePad1.SetVibration(60, 60);
			}
			else if (timerSqueeze0 >= 2 * squeezeTime / 5 && timerSqueeze0 < 3 * squeezeTime / 5)
			{
				gamePad1.SetVibration(35, 35);
			}
			else if (timerSqueeze0 >= 1 * squeezeTime / 5 && timerSqueeze0 < 2 * squeezeTime / 5)
			{
				gamePad1.SetVibration(15, 15);
			}
			else if (timerSqueeze0 > 0 && timerSqueeze0 < squeezeTime / 5)
			{
				gamePad1.SetVibration(5, 5);
			}

			if (gamePad1.RightTrigger == 0 && timerSqueeze0 > 0)
			{
				timerSqueeze0 = 5;
			}

			if (gamePad2.RightTrigger == 0 && timerSqueeze1 > 0)
			{
				timerSqueeze1 = 5;
			}

			if (timerSqueeze1 >= squeezeTime)
			{
				gamePad2.StopVibration();
			}
			else if (timerSqueeze1 >= 4 * squeezeTime / 5 && timerSqueeze1 < squeezeTime)
			{
				gamePad2.SetVibration(90, 90);
			}
			else if (timerSqueeze1 >= 3 * squeezeTime / 5 && timerSqueeze1 < 4 * squeezeTime / 5)
			{
				gamePad2.SetVibration(60, 60);
			}
			else if (timerSqueeze1 >= 2 * squeezeTime / 5 && timerSqueeze1 < 3 * squeezeTime / 5)
			{
				gamePad2.SetVibration(35, 35);
			}
			else if (timerSqueeze1 >= 1 * squeezeTime / 5 && timerSqueeze1 < 2 * squeezeTime / 5)
			{
				gamePad2.SetVibration(15, 15);
			}
			else if (timerSqueeze1 > 0 && timerSqueeze1 < squeezeTime / 5)
			{
				gamePad2.SetVibration(5, 5);
			}

			if (gamePad1.RightTrigger == 0)
			{
				gamePad1.StopVibration();
			}

			if (gamePad2.RightTrigger == 0)
			{
				gamePad2.StopVibration();
			}

			if (gamePad1.A.Pressed || gamePad2.A.Pressed)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}

		private void OnCollisionEnter(Collision other)
		{
			if(other.collider.CompareTag("Mecha"))
			{
				soulMate.GetComponent<SphereCollider>().enabled = false;
				this.GetComponent<SphereCollider>().enabled = false;
				IsOrgaGlowing = true;
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
			}
		}
	}
}