using Flusk.Effect.Particles;
using Flusk.Effect.Spheres;
using Flusk.Extensions;
using Flusk.Utility;
using TwoOfUs.Player;
using TwoOfUs.Player.Characters;
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
            if (parentCreator is Orga)
            {
                (parentCreator as Orga).OrgaGlowingChanged += Sparkle;
            }
            else if ( parentCreator is Mecha)
            {
                (parentCreator as Mecha).MechaGlowingChanged += Sparkle;
            }
        }

        protected virtual void OnDisable()
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
    }
}