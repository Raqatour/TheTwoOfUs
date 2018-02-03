namespace Flusk.Helpers
{
    public static class Numbers
    {
        public static float Remap(float min1, float max1, float value, float min2, float max2)
        {
            //low2 + (value - low1) * (high2 - low2) / (high1 - low1)
            float firstDifference = value - min1;
            float secondRange = (max2 - min2);
            float firstRange = (max1 - min1);
            float secondDifference = firstDifference * secondRange / firstDifference;
            float result = min2 + secondDifference;
            return result;
        }

        public static float Map(this float value, float min1, float max1, float min2, float max2)
        {
            return Remap(min1, max1, value, min2, max2);
        }
    }
}