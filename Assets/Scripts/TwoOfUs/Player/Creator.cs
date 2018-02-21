using System;
using Flusk.Utility;
using TwoOfUs.Management;
using TwoOfUs.Player.Audio;
using TwoOfUs.Player.Peripheral;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoOfUs.Player
{
	public abstract class Creator : TwoOfUsBehaviour
	{
		//TODO: make this an enum
		[SerializeField]
		protected int gender;
		public int Gender
		{
			get { return gender; }
		}
		
		public GameObject soulMate;
	
		// The Creator of the soul mate
		protected Creator SoulMate { get; set; }

		public Rigidbody Rigidbody { get; private set; }
		public float speed = 10.0f;
		
		[SerializeField]
		protected int ammo = 3;
		public int Ammo
		{
			get { return ammo; }
			set { ammo = value; }
		}

		protected SoundEffectController SoundFx { get; set; }

		[SerializeField]
		protected KeyCode anotherCharacterInhaleKey = KeyCode.V;

		protected TimeVibrationController vibrationController;

		public GamePadController.Controller GamepadController { get; private set; }

		private bool isIgnited;

		public virtual bool IsGlowing { get; set; }

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

		[SerializeField]
		protected float squeezeTime = 5.0f;
		public float timerSqueeze0;
		public float timerSqueeze1 = 0.0f;

		public Timer SqueezeTimer { get; protected set; }

		public GamePadController.Controller gamePad1;
		public GamePadController.Controller gamePad2;

		private Vector3 originalScale;

		public float Scale
		{
			get { return transform.localScale.x; }
		}

		public float TotalScale
		{
			get { return Scale * orbit.Scale; }
		}

		// TODO: store using the struct SizeRange
		
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

		public void ResetTimer()
		{
			if (SqueezeTimer != null)
			{
				SqueezeTimer.Reset();
			}
		}
		
		public void ForceFinishTimer(bool nullify = false)
		{			
			if (nullify)
			{
				SqueezeTimer = null;
				return;
			}
			
			if (SqueezeTimer != null)
			{
				SqueezeTimer.Tick(float.MaxValue);
			}
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

			vibrationController = GetComponentInChildren<TimeVibrationController>();
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
				IsGlowing = true;
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
			IsGlowing = true;
			soulMate.GetComponent<Creator>().IsGlowing = true;
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