using System;
using UnityEngine;

namespace TwoOfUs.Development.Tracking
{
    public class TimeTracker : ITracker
    {
        public float Min { get; private set; }
        public float Max { get; private set; }
        public float Current { get; private set; }

        public void Update(float time)
        {
            time = Mathf.RoundToInt(time * 1000);
            
            Min = Mathf.Min(Min, time);
            if (Math.Abs(Min) < float.Epsilon)
            {
                Min = time;
            }
            Max = Mathf.Max(Max, time);
            Current = time;
        }
        
        public override string ToString()
        {
            return string.Format("Min {0}: Max: {1} Current: {2}", Min, Max, Current);
        }
    }
}