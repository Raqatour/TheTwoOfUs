using System;

namespace Flusk.Utility
{
    public class Timer
    {
        public Action Complete;
        public Action<float> Update;

        public bool IsRunning { get; protected set; }

        public float Time { get; private set; }
        
        public float Goal { get; private set; }

        public Timer (float time, Action onComplete = null )
        {
            Goal = time;
            Complete = onComplete;
            IsRunning = true;
        }

        public virtual void Reset()
        {
            Time = 0;
            IsRunning = true;
        }

        public void Tick (float deltaTime)
        {
            if (!IsRunning)
            {
                return;
            }
            
            Time += deltaTime;
            if (Update != null)
            {
                Update(Time);
            }
            if ( Time > Goal )
            {
                IsRunning = false;
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
