namespace TwoOfUs.Development.Tracking
{
    public interface ITracker
    {
        float Max { get; }
        float Min { get; }
        float Current { get; }

        void Update(float time);


        string ToString();
    }
}