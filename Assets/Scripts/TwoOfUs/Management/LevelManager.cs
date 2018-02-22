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
        
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        public static void Assign()
        {
            //TODO: Find a more elegant solution
            Creator[] creators = FindObjectsOfType<Creator>();
            FindObjectOfType<LevelManager>().Force();
            foreach (var current in creators)
            {
                switch (current.Player)
                {
                    case Creator.PlayerHalf.Orga:
                        Instance.Orga = current;
                        break;
                    case Creator.PlayerHalf.Mecha:
                        Instance.Mecha = current;
                        break;
                }
            }
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
    }
}