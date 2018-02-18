using UnityEditor;
using UnityEngine;

namespace TwoOfUs.Player
{
    [CanEditMultipleObjects]
    public class ControllerCreator : TwoOfUsBehaviour
    {
        [SerializeField]
        protected GameObject [] controllers;
        
#if UNITY_EDITOR
        public
#else
        private
#endif
            void Spawn()
        {
            int length = controllers.Length;
            for (int i = 0; i < length; i++)
            {
                Instantiate(controllers[i], transform);
            }
        }  
        
        protected virtual void Awake()
        {
            Spawn();
        }
        
    }
}