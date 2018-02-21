using System;

namespace Flusk.Utility
{
    public class ModifyableValue<T>
    {
        private T data;

        public T Value
        {
            get { return data; }
            set
            {
                if (data.Equals(value))
                {
                    return;
                }

                if (ItemChanged != null)
                {
                    ItemChanged(data, value);
                }
                data = value;
            }
        }

        /// <summary>
        /// The event that fires off broacasting the change, and the previous and old data
        /// </summary>
        public Action<T, T> ItemChanged;


    }
}