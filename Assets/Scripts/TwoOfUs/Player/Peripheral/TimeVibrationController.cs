using UnityEngine;

namespace TwoOfUs.Player.Peripheral
{
    public class TimeVibrationController : TwoOfUsBehaviour, IPlayerController
    {
        [SerializeField]
        protected AnimationCurve vibrationCurve;

        /// <summary>
        /// For compensating the resolution of the curve
        /// </summary>
        [SerializeField]
        protected float curveMultiplier = 100;

        private float currentTime;

        private GamePadController.Controller gamepad;
        
        public GameObject GameObject
        {
            get { return gameObject; }
        }

        public Creator Creator { get; private set; }

        public void Init(Creator creator)
        {
            Creator = creator;
        }

        public void SetCurrentTime(float time)
        {
            if (time < 0)
            {
                Stop();
                return;
            }
            currentTime = time;
            float current = vibrationCurve.Evaluate(currentTime) * 100;
            Creator.GamepadHelper.SetVibration(current);
        }

        public void Stop()
        {
            gamepad.StopVibration();
        }

        public void Ready()
        {
            gamepad = Creator.GamepadHelper.Controller;
        }
    }
}