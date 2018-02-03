using System.Collections.Generic;
using UnityEngine;

namespace Flusk.Effect.Particles
{
    public class TrailManager : MonoBehaviour
    {
        private List<ParticleSystem> trails;

        protected virtual void Awake()
        {
            int count = transform.childCount;
            trails = new List<ParticleSystem>(count);
            GetComponentsInChildren(trails);
        }
    }
}