using Flusk.Patterns;
using Flusk.Utility;
using TwoOfUs.Flow;
using TwoOfUs.Management.Flow;
using UnityEngine;

namespace TwoOfUs.Management
{
    public class ResetManager : PersistentSingleton<ResetManager>
    {
        [SerializeField]
        protected ResetEffectController effectControllerTemplate;
        
        [SerializeField]
        protected float delay = 1.5f;

        private Timer timer;

        public void ResetLevel()
        {
            ResetEffectController effectController = Instantiate(effectControllerTemplate);
            effectController.Reset();
            timer = new Timer(delay, OnTimerEnd);
        }

        protected virtual void Update()
        {
            if (timer == null)
            {
                return;
            }
            timer.Tick(Time.deltaTime);
        }
        
        
        private void OnTimerEnd()
        {
            SceneManager sm;
            if (!SceneManager.TryGetInstance(out sm))
            {
                Debug.LogError("No SceneManager available");
                return;
            }
            sm.Load(sm.CurrentScene);
            timer = null;
        }
    }
}