using System;
using Flusk.Extensions;
using Flusk.Utility;
using UnityEngine;

namespace Flusk.Effect
{
    public class TimedEffectController : EffectController
    {
        protected Timer shrinkTimer, growTimer;

        public override void Sparkle(bool state)
        {
            base.Sparkle(state);
            if (state)
            {
                SetUpShrink();
            }
            else
            {
                SetUpGrow();
            }
        }   
        
        protected virtual void Update()
        {
            if (shrinkTimer != null)
            {
                shrinkTimer.Tick(Time.deltaTime);
            }

            if (growTimer != null)
            {
                growTimer.Tick(Time.deltaTime);
            }
        }

        private void SetUpShrink()
        {
            shrinkTimer = new Timer(shrinkCurve.GetFinalTime(), ShrinkComplete);
            shrinkTimer.Update = ShrinkUpdate;
        }

        private void ShrinkUpdate(float currentTime)
        {
            float current = shrinkCurve.Evaluate(currentTime);
            sparkleManager.SetSparkleScale(current);
            innerSphereManager.SetScale(current);
        }

        private void ShrinkComplete()
        {
            sparkleManager.SetSparkle(false);
            shrinkTimer = null;
        }

        private void SetUpGrow()
        {
            growTimer = new Timer(growCurve.GetFinalTime(), GrowComplete);
            innerSphereManager.Activate();
            growTimer.Update = GrowUpdate;
        }

        private void GrowUpdate(float currentTime)
        {
            float current = growCurve.Evaluate(currentTime);
            sparkleManager.SetSparkleScale(current);
            innerSphereManager.SetScale(current);
        }

        private void GrowComplete()
        {
            sparkleManager.SetSparkle(true);
            growTimer = null;
        }
    }
}