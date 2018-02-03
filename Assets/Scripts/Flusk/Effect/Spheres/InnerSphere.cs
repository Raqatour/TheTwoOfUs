using UnityEngine;

namespace Flusk.Effect.Spheres
{
    public class InnerSphere: Sphere
    {
        private float minScale, maxScale;

        public void Init(float min, float max)
        {
            minScale = Mathf.Min(min, max);
            maxScale = Mathf.Max(min, max);
        }
    }
}