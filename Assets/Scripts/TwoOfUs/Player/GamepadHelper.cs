using System;
using LaunchGamePadHelper;
using UnityEngine;

namespace TwoOfUs.Player
{
    // Write your controller rules here
    
    [Serializable]
    public class GamepadHelper
    {
        public GamePadController.Controller Controller { get; private set; }
        
        public Vector3 LeftStick
        {
            get { return Controller.LeftStick.GetVector3(); }
        }

        public bool IsSqueezing
        {
            get { return Mathf.Approximately(Controller.RightTrigger, 1) && Controller.B.Held; }
        }

        public bool IsSquezzingReleased
        {
            get { return Mathf.Approximately(Controller.RightTrigger, 0); }
        }

        public bool IsResetting
        {
            get { return Controller.A.Held; }
        }

        public bool IsGoingToMenu
        {
            get { return Controller.X.Pressed; }
        }

        public void SetVibration(float intensity)
        {
            Controller.SetVibration(intensity);
        }

        public void StopVibration()
        {
            Controller.StopVibration();
        }
        
        public GamepadHelper(GamePadController.Controller controller)
        {
            Controller = controller;
        }
    }
}