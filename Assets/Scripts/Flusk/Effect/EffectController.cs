using Flusk.Effect.Particles;
using Flusk.Effect.Spheres;
using Flusk.Extensions;
using Flusk.Utility;
using UnityEngine;

namespace Flusk.Effect
{
    public class EffectController: MonoBehaviour
    {
        [SerializeField]
        protected InnerSphereManager innerSphereManager;

        [SerializeField]
        protected OuterSphereManager outerSphereManager;

        [SerializeField]
        protected SparkleManager sparkleManager;

        [SerializeField]
        protected TrailManager trailFxManager;

        [SerializeField]
        protected TrailManager sparkleTrailFxManager;

        [SerializeField]
        protected AnimationCurve shrinkCurve, growCurve;

        protected Creator parentCreator;

        public virtual void Sparkle(bool state)
        {
            sparkleManager.SetSparkle(state);
            innerSphereManager.SetActive(state);
        }

        protected virtual void OnEnable()
        {
            parentCreator = GetComponentInParent<Creator>();
            parentCreator.IgnitedChanged += Sparkle;
        }

        protected virtual void OnDisable()
        {
            parentCreator.IgnitedChanged -= Sparkle; 
        }
    }
}