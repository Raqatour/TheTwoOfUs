using System;
using Flusk.Helpers;

namespace Flusk.Structures
{
    [Serializable]
    public struct Range
    {
        public float Min;
        public float Max;

        public float Median
        {
            get { return ((Min + Max) * 0.5f); }
        }

        public float Length
        {
            get { return (Max - Min); }
        }

        public bool WithinRange(float x)
        {
            return (x <= Max && x >= Min);
        }

        public float Map(float min, float max, float value)
        {
            return value.Map(min, max, Min, Max);
        }
    }
}