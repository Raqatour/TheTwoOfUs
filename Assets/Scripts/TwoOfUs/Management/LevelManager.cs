using System;
using Flusk.Patterns;
using TwoOfUs.Player;
using UnityEditor;
using UnityEngine;

namespace TwoOfUs.Management
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField]
        protected Creator orga;
        public Creator Orga
        {
            get { return orga; }
            protected set { orga = value; }
        }

        [SerializeField]
        protected Creator mecha;
        public Creator Mecha
        {
            get { return mecha; }
            protected set { mecha = value; }
        }

        public bool IsReady
        {
            get
            {
                return orga != null && orga.GamepadController != null
                                    && mecha != null && mecha.GamepadController != null;
            }
        }

        public static event Action CreatorFound;
        
#if UNITY_EDITOR
        public static void Assign()
        {
            
        }
#endif
        
        
        public void Force()
        {
            Awake();
        }

        public Creator GetOtherHalf(Creator.PlayerHalf half)
        {
            switch (half)
            {
                case Creator.PlayerHalf.Orga:
                    return Mecha;
                case Creator.PlayerHalf.Mecha:
                    return Orga;
            }
            return null;
        }
        
        public void GetController<T>(T creator) where T : Creator
        {
            if (creator.Player == Creator.PlayerHalf.Orga)
            {
                Orga = creator;
                Orga.AssignGamepad(GamePadController.GamePadOne);
                return;
            }

            if (creator.Player == Creator.PlayerHalf.Mecha)
            {
                Mecha = creator;
                Mecha.AssignGamepad(GamePadController.GamePadTwo);
            }
        }

        protected virtual void OnEnable()
        {
            //TODO: Find a more elegant solution
            Creator[] creators = FindObjectsOfType<Creator>();
            foreach (var current in creators)
            {
                switch (current.Player)
                {
                    case Creator.PlayerHalf.Orga:
                        Instance.Orga = current;
                        if (CreatorFound != null)
                        {
                            CreatorFound();
                        }
                        break;
                    case Creator.PlayerHalf.Mecha:
                        Instance.Mecha = current;
                        if (CreatorFound != null)
                        {
                            CreatorFound();
                        }
                        break;
                }
            }
        }
    }
}