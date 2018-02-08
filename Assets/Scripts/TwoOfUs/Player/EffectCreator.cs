using UnityEditor;
using UnityEngine;

namespace TwoOfUs.Player
{
    [CanEditMultipleObjects]
    public class EffectCreator : TwoOfUsBehaviour
    {
        [SerializeField]
        protected GameObject effect;

        [SerializeField, HideInInspector]
        protected GameObject spawn;
        
#if UNITY_EDITOR
        public
#else
        private
#endif
            void Spawn()
        {
            spawn = Instantiate(effect, transform);
        }  
        
        protected virtual void Awake()
        {
            if (spawn != null)
            {
                Destroy(spawn);
            }
            Spawn();
        }
        
    }
}