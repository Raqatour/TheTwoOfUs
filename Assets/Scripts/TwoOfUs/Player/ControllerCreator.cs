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

            var creator = GetComponent<Creator>();
            
            for (int i = 0; i < length; i++)
            {
                var current = controllers[i].GetComponent<IPlayerController>();
                var created = Instantiate(current.GameObject, transform);
                created.GetComponent<IPlayerController>().Init(creator);
            }
        }  
        
        protected virtual void Awake()
        {
            Spawn();
        }
        
    }
}