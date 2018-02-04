﻿using System;

namespace Flusk.Utility
{
    public class Timer
    {
        public Action Complete;
        public Action<float> Update;

        private float time = 0;
        private float goal = 0;

        public Timer (float time, Action onComplete = null )
        {
            goal = time;
            Complete = onComplete;
        }

        public virtual void Reset()
        {
            time = 0;
        }

        public void Tick (float deltaTime)
        {
            time += deltaTime;
            if (Update != null)
            {
                Update(time);
            }
            if ( time > goal )
            {
                Fire();
            }
        }

        private void Fire ()
        {
            if ( Complete != null )
            {
                Complete();
            }
        }
        
    }
}
