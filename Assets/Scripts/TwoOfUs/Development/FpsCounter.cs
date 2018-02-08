using Flusk.Patterns;
using TwoOfUs.Development.Tracking;
using UnityEngine;

namespace TwoOfUs.Development
{
    public class FpsCounter : Singleton<FpsCounter>
    {
        private FpsTracker fps = new FpsTracker();
        private TimeTracker timeTracker = new TimeTracker();

        private float delay = 0.5f;
        private float start;
        
        protected virtual void Update()
        {
            if (Time.timeSinceLevelLoad < delay)
            {
                return;
            }
            
            fps.Update(Time.deltaTime);
            timeTracker.Update(Time.deltaTime);
        }

        protected virtual void OnGUI()
        {
            float width = Screen.width * 0.3f;
            float height = Screen.height * 0.05f;

            GUI.skin.box.fontSize = 24;
            
            GUI.Box(new Rect(width * 0.5f, 0, width, height), fps.ToString());
            GUI.Box(new Rect(width * 0.5f, height, width, height), timeTracker.ToString());
        }
    }
}