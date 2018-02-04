using UnityEngine;

namespace Flusk.Effect.Particles
{
    public class PrimaryParticlesController : MonoBehaviour
    {
        [SerializeField]
        protected ParticleSystem main;

        [SerializeField]
        protected ParticleSystem support;

        private Vector3 maxScale;

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        public void SetScale(float ratio)
        {
            transform.localScale = maxScale * ratio;
        }
        

        protected virtual void Awake()
        {
            maxScale = transform.localScale;
        }
    }
}