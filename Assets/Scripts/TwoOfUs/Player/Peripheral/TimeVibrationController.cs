using UnityEngine;

namespace TwoOfUs.Player.Peripheral
{
    public class TimeVibrationController : TwoOfUsBehaviour
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

        public void SetCurrentTime(float time)
        {
            if (time < 0)
            {
                Stop();
                return;
            }
            currentTime = time;
            float current = vibrationCurve.Evaluate(currentTime) * 100;
            gamepad.SetVibration(current);
        }

        public void Stop()
        {
            gamepad.StopVibration();
        }

        private void Start()
        {
            Creator creator = GetComponentInParent<Creator>();
            gamepad = creator.GamepadController;
        }
    }
}