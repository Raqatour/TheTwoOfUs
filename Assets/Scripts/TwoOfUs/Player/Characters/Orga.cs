using System;
using Flusk.Utility;
using LaunchGamePadHelper;
using TwoOfUs.Management;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoOfUs.Player.Characters
{
    public class Orga : Creator
    {
        public bool inhaled = true;

        [SerializeField]
        protected bool isOrgaGlowing = true;

        public override bool IsGlowing
        {
            get { return isOrgaGlowing; }
            set { IsOrgaGlowing = value; }
        }

        public bool isOrgaBig;

        public bool IsOrgaGlowing
        {
            get { return isOrgaGlowing; }
            set
            {
                if (OrgaGlowingChanged != null)
                {
                    OrgaGlowingChanged(value);
                }
                isOrgaGlowing = IsGlowing = value;
            }
        }

        protected override void Start()
        {
            base.Start();
            
            LevelManager levelManager;
            if (!LevelManager.TryGetInstance(out levelManager))
            {
                Debug.LogError("No LevelManager in the scene");
            }

            SoulMate = levelManager.Mecha;
            soulMate = SoulMate.gameObject;
        }

        protected override void Movement()
        {
            base.Movement();
            if (!isEnded)
            {
                OrgaMovement();
            }
        }
        

        private void OrgaMovement()
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

            Rigidbody.velocity = GamepadController.LeftStick.GetVector3() * speed;

            // Collider size change
            if (Input.GetKey(anotherCharacterInhaleKey) || GamepadController.RightTrigger == 1 && GamepadController.B.Held)
            {
                if (SqueezeTimer == null || !SqueezeTimer.IsRunning)
                {
                    SqueezeTimer = new Timer(squeezeTime, OnTimerComplete);
                    SqueezeTimer.Update = OnTimerUpdate;
                }
                
                SqueezeTimer.Tick(Time.deltaTime);

                if (!IsIgnited && SqueezeTimer.IsRunning && IsOrgaGlowing )
                {
                    isOrgaBig = true;
                }
                
                //TODO: This is probably going to run waaaaay tooo often
                if (isOrgaBig && SqueezeTimer.IsRunning)
                {
                    SoulMate.Ammo = 1;
                    Ammo = 6;
                    SoundFx.Exhale();
                }
            }

            // Creator is big and player let go
            if (isOrgaBig && GamepadController.RightTrigger == 0)
            {
                isOrgaGlowing = isOrgaBig = false;
            }

            if (Input.GetKeyUp(anotherCharacterInhaleKey) || GamepadController.RightTrigger == 0 || !SqueezeTimer.IsRunning)
            {
                soulMate.GetComponent<Creator>().Ammo = 3;
                Ammo = 3;
            }
            
            GamePadeOneCheck();
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
            
            GamepadController.StopVibration();
            
        }

        protected void GamePadeOneCheck()
        {
            if (gamePad1.RightTrigger == 0 && SqueezeTimer.IsRunning)
            {
                ForceFinishTimer();
            }
			
            if (GamepadController.X.Pressed)
            {
                SceneManager.LoadScene(0);
            }
			
            if (GamepadController.RightTrigger == 0)
            {
                gamePad1.StopVibration();
            }
        }

        private void OnTimerUpdate(float time)
        {
            vibrationController.SetCurrentTime(time);
        }

        public event Action<bool> OrgaGlowingChanged;
    }
}