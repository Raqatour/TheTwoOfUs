using Flusk.Structures;
using UnityEngine;

namespace Flusk.Effect.Spheres
{
    public class InnerSphereManager : SphereManager<InnerSphere>
    {
        [SerializeField]
        protected Range scaleSizes;

        protected Vector3 orginialScale;

        public void SetScale(float size)
        {
            transform.localScale = orginialScale * size;
        }
        
        protected override void Awake()
        {
            base.Awake();
            orginialScale = transform.localScale;
            int count = spheres.Count;
            for (int i = 0; i < count; i++)
            {
                InnerSphere current = spheres[i];
                current.Init(scaleSizes.Min, scaleSizes.Max);
            }
        }
    }
}