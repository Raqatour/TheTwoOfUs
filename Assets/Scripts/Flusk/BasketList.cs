using System;
using System.Collections.Generic;

namespace Flusk
{
    [Serializable]
    public class BasketList<T>
    {
        public int BasketCount { get; protected set; }

        public int Count
        {
            get
            {
                // gross
                int sum = 0;
                foreach (var list in data)
                {
                    sum += list.Count;
                }
                return sum;
            }
        }

        public List<T> this[int index]
        {
            get { return data[index]; }
        }

        protected List<List<T>> data;

        protected int currentIndexAdd;

        public BasketList(int count)
        {
            BasketCount = count;
            data = new List<List<T>>(count);
            for (int i = 0; i < count; i++)
            {
                data.Add(new List<T>());
            }

            currentIndexAdd = 0;
        }

        public void Add(T info)
        {
            data[currentIndexAdd].Add(info);
            currentIndexAdd++;
            currentIndexAdd = currentIndexAdd % BasketCount;
        }
    }
}