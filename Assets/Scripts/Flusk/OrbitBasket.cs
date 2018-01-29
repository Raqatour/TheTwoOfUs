using System;

namespace Flusk
{
    [Serializable]
    public class OrbitBasket : BasketList<Orbit>
    {
        public int CurrentFrame { get; protected set; }
        
        public OrbitBasket(int count) : base(count)
        {
        }

        public void Update(int index)
        {
            var list = this[index];
            foreach (var orbit in list)
            {
                orbit.UpdateOrbit();
            }
        }

        public void UpdateNext()
        {
            Update(CurrentFrame);
            CurrentFrame++;
            CurrentFrame = CurrentFrame % BasketCount;
        }
    }
}