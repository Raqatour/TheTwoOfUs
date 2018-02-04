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

        public void Sparkle(bool state)
        {
            sparkleManager.Sparkle(state);
            innerSphereManager.gameObject.SetActive(state);
        }
    }
}