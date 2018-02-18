using System;
using TwoOfUs.Management;
using TwoOfUs.Player.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoOfUs.Player
{
	public class Creator : TwoOfUsBehaviour
	{	
		public int gender;
		public GameObject soulMate;
	
		// The Creator of the soul mate
		protected Creator SoulMate { get; set; }
	
	
		public Rigidbody Rigidbody { get; private set; }
		public float speedy = 10.0f;
		public int ammo = 3;

		protected SoundEffectController SoundFx { get; set; }
	
		[SerializeField]
		protected bool isOrgaGlowing = true;
	
		[SerializeField]
		protected bool isMechaGlowing = true;

		[SerializeField]
		protected KeyCode AnotherCharacterInhaleKey = KeyCode.V;

		public GamePadController.Controller GamepadController { get; private set; }

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

		public void AssignGamepad(GamePadController.Controller controller)
		{
			GamepadController = controller;
		}
		
		protected virtual void Awake()
		{
			SoundFx = GetComponentInChildren<SoundEffectController>();
			orbit = GetComponentInChildren<Orbit>();
		}

		protected virtual void Start()
		{
			LevelManager levelManager;
			if (!LevelManager.TryGetInstance(out levelManager))
			{
				Debug.LogError("No LevelManager in the scene");
			}
			
			levelManager.GetController(this);
			
			gamePad1 = GamePadController.GamePadOne;
			gamePad2 = GamePadController.GamePadTwo;

			originalScale = transform.localScale;
		
			Rigidbody = GetComponent<Rigidbody>();

			
		}

		protected virtual void Update()
		{
			// Update orbits
			orbit.UpdateOrbit();	
			
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

		protected virtual void Movement()
		{
			if (isEnded)
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

		protected void SetScale(float size)
		{
			transform.localScale = originalScale * size;
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
			if (!other.collider.CompareTag("Mecha"))
			{
				return;
			}

			soulMate.GetComponent<SphereCollider>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			IsOrgaGlowing = true;
			soulMate.GetComponent<Creator>().isMechaGlowing = true;
			Instantiate(Ender, new Vector3((transform.position.x + other.transform.position.x)/2, (transform.position.y + other.transform.position.y)/2, 0), Quaternion.identity);
			heart = GameObject.FindWithTag("Heart");
			if (heart == null)
			{
				return;
			}

			SoundFx.Whoosh(0.5f);
			soulMate.transform.parent = heart.transform;
			transform.parent = heart.transform;
			isEnded = true;
			soulMate.GetComponent<Creator>().isEnded = true;
		}
	}
}