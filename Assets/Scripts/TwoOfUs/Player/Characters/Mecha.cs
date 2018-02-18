using System;
using TwoOfUs.Management;
using UnityEngine;

namespace TwoOfUs.Player.Characters
{
    public class Mecha : Creator
    {
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

            Rigidbody.velocity = new Vector3(GamepadController.LeftStick.X * -speedy, GamepadController.LeftStick.Y * -speedy, 0);

            if (Input.GetKey(KeyCode.V) || GamepadController.RightTrigger == 1 && GamepadController.B.Held)
            {
                timerSqueeze1 += Time.deltaTime;
                if (isMechaBig && timerSqueeze1 < squeezeTime)
                {
                    soulMate.GetComponent<Creator>().ammo = 1;
                    ammo = 6;
                    SoundFx.Exhale();
                }
            }

            if (Input.GetKeyUp(AnotherCharacterInhaleKey) || GamepadController.RightTrigger == 0 || timerSqueeze1 >= squeezeTime)
            {
                soulMate.GetComponent<Creator>().ammo = 3;
                ammo = 3;
            }

            if (timerSqueeze1 >= squeezeTime && mechaInhaled == true)
            {
                SoundFx.Inhale();
                mechaInhaled = false;
            }
        }
    }
}