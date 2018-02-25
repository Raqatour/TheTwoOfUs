using System;
using System.Collections;
using Flusk.Utility;
using LaunchGamePadHelper;
using TwoOfUs.Management;
using TwoOfUs.Player.Audio;
using TwoOfUs.Player.Peripheral;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoOfUs.Player
{
	public class Creator : TwoOfUsBehaviour
	{
		public enum PlayerHalf
		{
			Orga,
			
			Mecha
		}

		//TODO: make this an enum
		[SerializeField]
		protected PlayerHalf player;
		public PlayerHalf Player
		{
			get { return player; }
		}
		
		public event Action<bool> GlowingChanged;
		
		public GameObject soulMate;
	
		// The Creator of the soul mate
		public Creator SoulMate { get; private set; }

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

		//public GamePadController.Controller GamepadController { get; private set; }
		public GamepadHelper GamepadHelper { get; private set; }

		private bool isIgnited;

		public bool IsSqueezeTimerRunning
		{
			get { return SqueezeTimer != null && SqueezeTimer.IsRunning; }
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

		public SpinAroundSelf Ender;
		public GameObject heart;

		[SerializeField]
		protected float squeezeTime = 5.0f;

		public Timer SqueezeTimer { get; protected set; }


		private Vector3 originalScale;

		public float Scale
		{
			get { return transform.localScale.x; }
		}

		public float TotalScale
		{
			get { return Scale * orbit.Scale; }
		}

		public bool inhaled = true;
		public bool isBig;
		[SerializeField]
		protected bool isGlowing = true;
		
		public bool IsGlowing
		{
			get { return isGlowing; }
			set
			{
				if (isGlowing == value)
				{
					return;
				}
				if (GlowingChanged != null)
				{
					GlowingChanged(value);
				}
				isGlowing = value;
			}
		}

		public bool IsReady
		{
			get { return GamepadHelper != null; }
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
			GamepadHelper = new GamepadHelper(controller);
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
			originalScale = transform.localScale;
			Rigidbody = GetComponent<Rigidbody>();
		}

		protected virtual void Start()
		{
			SoundFx = GetComponentInChildren<SoundEffectController>();
			orbit = GetComponentInChildren<Orbit>();
			var light = GetComponent<Light>();
			orbit.Init(this, light);
			vibrationController = GetComponentInChildren<TimeVibrationController>();
		}

		private void OnCreatorFound()
		{
			LevelManager levelManager;
			if (!LevelManager.TryGetInstance(out levelManager))
			{
				Debug.LogError("No LevelManager in the scene");
			}

			SoulMate = levelManager.GetOtherHalf(player);
			if (SoulMate != null)
			{
				soulMate = SoulMate.gameObject;
			}
			levelManager.GetController(this);
		}

		protected virtual void OnEnable()
		{
			LevelManager.CreatorFound += OnCreatorFound;
		}

		protected virtual void OnDisable()
		{
			LevelManager.CreatorFound -= OnCreatorFound;
		}

		protected virtual void Update()
		{
			LevelManager lm;
			if (!LevelManager.TryGetInstance(out lm))
			{
				return;
			}

			if (lm.IsReady)
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
			else
			{
				if (Ammo == 1)
				{
					SetScale(minimumSize);
				}
				else if (Ammo == 6)
				{
					SetScale(maxSize);
				}
				else if (Ammo == 3)
				{
					SetScale(mediumSize);
				}

				Rigidbody.velocity = GamepadHelper.LeftStick * speed;

				// Collider size change
				if (Input.GetKey(anotherCharacterInhaleKey) || GamepadHelper.IsSqueezing)
				{
					if (!IsSqueezeTimerRunning)
					{
						SqueezeTimer = new Timer(squeezeTime, OnTimerComplete);
						SqueezeTimer.Update = OnTimerUpdate;
					}
                
					SqueezeTimer.Tick(Time.deltaTime);

					if (!IsIgnited && IsSqueezeTimerRunning && IsGlowing )
					{
						isBig = true;
					}
                
					//TODO: This is probably going to run waaaaay tooo often
					if (isBig && IsSqueezeTimerRunning)
					{
						SoulMate.Ammo = 1;
						Ammo = 6;
						SoundFx.Exhale();
					}
				}

				// Creator is big and player let go
				if (isBig && GamepadHelper.IsSquezzingReleased)
				{
					isGlowing = isBig = false;
				}

				if (Input.GetKeyUp(anotherCharacterInhaleKey) || GamepadHelper.IsSquezzingReleased || !IsSqueezeTimerRunning)
				{
					soulMate.GetComponent<Creator>().Ammo = 3;
					Ammo = 3;
				}
            
				GamePadeCheck();
			}
		}
		
		private void OnTimerComplete()
		{
			if (!inhaled)
			{
				return;
			}
			SoundFx.Stop();
			SoundFx.Inhale();
			inhaled = false;
            
			GamepadHelper.StopVibration();
            
		}

		protected void SetScale(float size)
		{
			transform.localScale = originalScale * size;
		}

		private void GamePadCheck()
		{			
			if (GamepadHelper.IsResetting)
			{
				ResetScene();
			}
		}

		private static void ResetScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
			Instantiate(Ender, new Vector3((transform.position.x + other.transform.position.x)/2, 
				(transform.position.y + other.transform.position.y)/2, 0), Quaternion.identity);
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

		protected void GamePadeCheck()
		{
			if (GamepadHelper.IsSquezzingReleased && IsSqueezeTimerRunning )
			{
				ForceFinishTimer();
			}
			
			if (GamepadHelper.IsGoingToMenu)
			{
				SceneManager.LoadScene(0);
			}
			
			if (GamepadHelper.IsSquezzingReleased)
			{
				GamepadHelper.StopVibration();
			}
		}

		private void OnTimerUpdate(float time)
		{
			vibrationController.SetCurrentTime(time);
		}	
	}
}