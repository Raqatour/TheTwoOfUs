using Flusk.Extensions;
using Flusk.Utility;
using TwoOfUs.Player;
using TwoOfUs.Player.Characters;
using UnityEngine;

namespace Flusk.Effect
{
    public class TimedEffectController : EffectController
    {
        private Timer shrinkTimer;
        private Timer growTimer;

        public override void Sparkle(bool state)
        {
            if (state == sparkleManager.IsSparkling)
            {
                return;
            }
            
            if (shrinkTimer != null || growTimer != null)
            {
                return;
            }
            base.Sparkle(state);
            if (!state)
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

        protected override void OnEnable()
        {
            parentCreator = GetComponentInParent<Creator>();
            if (parentCreator.Gender == 0)
            {
                (parentCreator as Orga).OrgaGlowingChanged += Sparkle;
            }
            else if ( parentCreator.Gender == 1)
            {
                (parentCreator as Mecha).MechaGlowingChanged += Sparkle;
            }
        }

        protected override void OnDisable()
        {
            if (parentCreator is Orga)
            {
                (parentCreator as Orga).OrgaGlowingChanged -= Sparkle;
            }
            else if ( parentCreator is Mecha)
            {
                (parentCreator as Mecha).MechaGlowingChanged -= Sparkle;
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
            SetScale(current);
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
            SetScale(current);
        }

        private void SetScale(float current)
        {
            sparkleManager.SetSparkleScale(current);
            innerSphereManager.SetScale(current);
            sparkleTrailFxManager.SetScale(current);
        }

        private void GrowComplete()
        {
            sparkleManager.SetSparkle(true);
            growTimer = null;
        }
    }
}