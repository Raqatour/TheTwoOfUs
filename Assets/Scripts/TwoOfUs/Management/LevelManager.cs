using System;
using Flusk.Patterns;
using TwoOfUs.Player;
using TwoOfUs.Player.Characters;

namespace TwoOfUs.Management
{
    public class LevelManager : Singleton<LevelManager>
    {
        public Creator Orga { get; private set; }
        public Creator Mecha { get; private set; }
        
        public void Force()
        {
            Awake();
        }
        

        public void GetController<T>(T creator) where T : Creator
        {
            if (creator.GetType() == typeof(Orga))
            {
                Orga = creator;
                Orga.AssignGamepad(GamePadController.GamePadOne);
                return;
            }

            if (creator.GetType() == typeof(Mecha))
            {
                Mecha = creator;
                Mecha.AssignGamepad(GamePadController.GamePadTwo);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            //TODO: Craft a more elegant solution for this
            Orga = FindObjectOfType<Orga>();
            Mecha = FindObjectOfType<Mecha>();
        }
    }
}