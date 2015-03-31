using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKP
{
    class CappedStack<T> : LinkedList<T>
    {
        private int capacity;

        public CappedStack(int capacity)
        {
            this.capacity = capacity;
        }

        public void Push(T item)
        {
            this.AddFirst(item);

            if (this.Count > capacity && capacity != 0)
                this.RemoveLast();
        }

        public T Pop()
        {
            var item = this.First.Value;
            this.RemoveFirst();
            return item;
        }

        public T Peek()
        {
            if (this.First == null)
            {
                return default(T);
            }
            return this.First.Value;
        }
    }
}
