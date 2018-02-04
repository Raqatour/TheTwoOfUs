using Flusk.Effect.Particles;
using Flusk.Effect.Spheres;
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

        private Creator parentCreator;

        public void Sparkle(bool state)
        {
            sparkleManager.Sparkle(state);
            innerSphereManager.gameObject.SetActive(state);
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