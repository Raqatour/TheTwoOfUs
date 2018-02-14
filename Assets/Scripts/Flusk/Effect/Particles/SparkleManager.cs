using UnityEngine;

namespace Flusk.Effect.Particles
{
    public class SparkleManager : MonoBehaviour
    {
        public bool IsSparkling
        {
            get { return sparkle.isActiveAndEnabled; }
        }
        
        [SerializeField]
        protected SparkleParticles sparkle;
        public SparkleParticles Sparkle
        {
            get { return sparkle; }
        }

        [SerializeField]
        protected NoSparkleParticles noSparkle;
        public NoSparkleParticles NoSparkle
        {
            get { return noSparkle; }
        }

        public void SetSparkleScale(float size)
        {
            sparkle.Activate();
            sparkle.SetScale(size);
            noSparkle.Activate();
        }
        
        public void SetSparkle(bool sparkling)
        {
            sparkle.SetActive(sparkling);
            noSparkle.SetActive(!sparkling);
        }
    }
}