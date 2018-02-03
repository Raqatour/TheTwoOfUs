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
        protected MainParticleSystem mainPfx;

        [SerializeField]
        protected SupportParticleSystem supportPfx;

        [SerializeField]
        protected TrailManager trailFxManager;

        protected virtual void Awake()
        {
            supportPfx.SetState(SupportParticleSystem.State.Pulsing);
        }
    }
}