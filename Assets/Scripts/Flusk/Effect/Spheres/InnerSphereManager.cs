using Flusk.Structures;
using UnityEngine;

namespace Flusk.Effect.Spheres
{
    public class InnerSphereManager : SphereManager<InnerSphere>
    {
        [SerializeField]
        protected Range scaleSizes;

        protected override void Awake()
        {
            base.Awake();
            int count = spheres.Count;
            for (int i = 0; i < count; i++)
            {
                InnerSphere current = spheres[i];
                current.Init(scaleSizes.Min, scaleSizes.Max);
            }
        }
    }
}