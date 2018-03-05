using Flusk.Utility;
using Reset_Scene_Transition;
using TwoOfUs.Management;
using UnityEngine;

namespace TwoOfUs.Flow
{
    public class ResetEffectController : TwoOfUsBehaviour
    {
        [SerializeField]
        protected string screenShotPath = "BattleTransitionScreenshotController";

        

        public void Reset()
        {
            Screenshot screenShot = Resources.Load<Screenshot>(screenShotPath);
            Screenshot created = Instantiate(screenShot);
            created.TakeSnapShot();
            Destroy();
        }

        
    }
}