using System.Collections.Generic;
using UnityEngine;

namespace Flusk.Effect.Particles
{
    public class TrailManager : MonoBehaviour
    {
        private List<ParticleSystem> trails;

        private Vector3 originalScale;
        
        public void SetScale(float scale)
        {
            transform.localScale = originalScale * scale;
        }
        
        protected virtual void Awake()
        {
            int count = transform.childCount;
            trails = new List<ParticleSystem>(count);
            GetComponentsInChildren(trails);

            originalScale = transform.localScale;
        }
    }
}