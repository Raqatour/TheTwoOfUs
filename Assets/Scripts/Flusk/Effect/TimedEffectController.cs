using Flusk.Extensions;
using Flusk.Utility;
using TwoOfUs.Player;
using UnityEngine;

namespace Flusk.Effect
{
    public class TimedEffectController : EffectController, IPlayerController
    {
        private Timer shrinkTimer;
        private Timer growTimer;
        
        public GameObject GameObject
        {
            get { return gameObject; }
        }

        public Creator Creator { get; private set; }

        public void Init(Creator creator)
        {
            Creator = creator;
        }

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

        protected void Start()
        {
            parentCreator = GetComponentInParent<Creator>();
            parentCreator.GlowingChanged += Sparkle;
        }

        protected void OnDestroy()
        {
            parentCreator = GetComponentInParent<Creator>();
            if (parentCreator == null)
            {
                return;
            }
            parentCreator.GlowingChanged -= Sparkle;
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