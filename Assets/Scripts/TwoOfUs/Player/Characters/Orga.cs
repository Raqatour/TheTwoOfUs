using System;
using TwoOfUs.Management;
using UnityEngine;

namespace TwoOfUs.Player.Characters
{
    public class Orga : Creator
    {
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

            Rigidbody.velocity = new Vector3(GamepadController.LeftStick.X * speedy, GamepadController.LeftStick.Y * speedy, 0);

            //Collider size change
            if (Input.GetKey(KeyCode.M) || GamepadController.RightTrigger == 1 && GamepadController.B.Held)
            {
                timerSqueeze0 += Time.deltaTime;
                if (isOrgaBig && timerSqueeze0 < squeezeTime)
                {
                    SoulMate.ammo = 1;
                    ammo = 6;
                    SoundFx.Exhale();
                }
            }

            if (Input.GetKeyUp(AnotherCharacterInhaleKey) || GamepadController.RightTrigger == 0 || timerSqueeze0 >= squeezeTime)
            {
                soulMate.GetComponent<Creator>().ammo = 3;
                ammo = 3;
            }

            if (timerSqueeze0 >= squeezeTime && orgaInhaled == true)
            {
                SoundFx.Stop();
                SoundFx.Inhale();
                orgaInhaled = false;
            }
        }
    }
}