using UnityEngine;

namespace Flusk.Effect.Particles
{
    public class SparkleManager : MonoBehaviour
    {
        [SerializeField]
        protected SparkleParticles sparkle;

        [SerializeField]
        protected NoSparkleParticles noSparkle;

        public void Sparkle(bool sparkling)
        {
            sparkle.gameObject.SetActive(sparkling);
            noSparkle.gameObject.SetActive(!sparkling);
        }

        protected virtual void Awake()
        {
            Sparkle(true);
        }
    }
}