using System;
using TwoOfUs.Management;
using UnityEngine;

namespace TwoOfUs.Player.Characters
{
    public class Mecha : Creator
    {
        public bool mechaInhaled = true;
        public bool isMechaBig;

        [SerializeField]
        protected bool isMechaGlowing = true;
        
        public override bool IsGlowing
        {
            get { return isMechaGlowing; }
            set { IsMechaGlowing = value; }
        }

        public bool IsMechaGlowing
        {
            get { return isMechaGlowing; }
            set
            {
                if  ( MechaGlowingChanged != null)
                {
                    MechaGlowingChanged(value);
                }
                isMechaGlowing = IsGlowing = value;
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

            SoulMate = levelManager.Orga;
            soulMate = SoulMate.gameObject;
        }

        protected override void Movement()
        {
            base.Movement();
            if (!isEnded)
            {
                MechaMovement();
            }
        }

        private void MechaMovement()
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

            Rigidbody.velocity = new Vector3(GamepadController.LeftStick.X * -speed, GamepadController.LeftStick.Y * -speed, 0);

            if (Input.GetKey(anotherCharacterInhaleKey) || GamepadController.RightTrigger == 1 && GamepadController.B.Held)
            {
                timerSqueeze1 += Time.deltaTime;
                if (isMechaBig && timerSqueeze1 < squeezeTime)
                {
                    soulMate.GetComponent<Creator>().Ammo = 1;
                    Ammo = 6;
                    SoundFx.Exhale();
                }
            }

            if (Input.GetKeyUp(anotherCharacterInhaleKey) || GamepadController.RightTrigger == 0 || timerSqueeze1 >= squeezeTime)
            {
                soulMate.GetComponent<Creator>().Ammo = 3;
                Ammo = 3;
            }

            if (timerSqueeze1 >= squeezeTime && mechaInhaled == true)
            {
                SoundFx.Inhale();
                mechaInhaled = false;
            }
        }

        private void GamePadTwoCheck()
        {
            if (gamePad2.RightTrigger == 0 && timerSqueeze1 > 0)
            {
                timerSqueeze1 = 5;
            }

            if (timerSqueeze1 >= squeezeTime)
            {
                gamePad2.StopVibration();
            }
            else if (timerSqueeze1 >= 0.8f * squeezeTime && timerSqueeze1 < squeezeTime)
            {
                gamePad2.SetVibration(90, 90);
            }
            else if (timerSqueeze1 >= 0.6f * squeezeTime && timerSqueeze1 < 4 * squeezeTime / 5)
            {
                gamePad2.SetVibration(60, 60);
            }
            else if (timerSqueeze1 >= 0.4f * squeezeTime && timerSqueeze1 < 3 * squeezeTime / 5)
            {
                gamePad2.SetVibration(35, 35);
            }
            else if (timerSqueeze1 >= 0.2f * squeezeTime && timerSqueeze1 < 2 * squeezeTime / 5)
            {
                gamePad2.SetVibration(15, 15);
            }
            else if (timerSqueeze1 > 0 && timerSqueeze1 < squeezeTime / 5)
            {
                gamePad2.SetVibration(5, 5);
            }

			

            if (gamePad2.RightTrigger == 0)
            {
                gamePad2.StopVibration();
            }
        }

        public event Action<bool> MechaGlowingChanged;
    }
}