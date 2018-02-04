using System.Collections.Generic;
using UnityEngine;

namespace Flusk.Effect.Spheres
{
    public class SphereManager<T>: MonoBehaviour where T : Sphere
    {
        protected List<T> spheres;
        
        protected virtual void Awake()
        {
            int count = transform.childCount;
            spheres = new List<T>(count);
            GetComponentsInChildren(spheres); 
        }
        
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
    }
}