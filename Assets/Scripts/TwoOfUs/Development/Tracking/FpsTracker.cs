
using UnityEngine;

namespace TwoOfUs.Development.Tracking
{
    public class FpsTracker : ITracker
    {
        public float Min { get; private set; }
        public float Max { get; private set; }
        public float Current { get; private set; }
        
        public void Update(float time)
        {
            float frames = Mathf.Round(1 / time);
            if (Min == 0)
            {
                Min = frames;
            }
            else
            {
                Min = Mathf.Min(Min, frames);
            }
            Max = Mathf.Max(Max, frames);
            Current = frames;
        }

        public override string ToString()
        {
            return string.Format("Min {0}: Max: {1} Current: {2}", Min, Max, Current);
        }
    }
}