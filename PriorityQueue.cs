using System;

namespace PDG.DataStructures
{
    /// <summary>
    /// Basic priority queue, which uses array-based BinaryHeap to store elements.
    /// </summary>
    class PriorityQueue<T>
    {
        BinaryHeap<ItemContainer> _data;

        public PriorityQueue(bool descending = false)
        {
            _data = new BinaryHeap<ItemContainer>(descending);
        }
        public void Insert(T item, float priority)
        {
            _data.Insert(new ItemContainer(item, priority));
        }
        public T Extract()
        {
            return _data.Extract().Item;
        }
        public T Top()
        {
            return _data.Top().Item;
        }
        public int Count => _data.Count;
        struct ItemContainer : IComparable<ItemContainer>
        {
            T _item;
            float _priority;
            public ItemContainer(T item, float priority)
            {
                _item = item;
                _priority = priority;
            }
            int IComparable<ItemContainer>.CompareTo(ItemContainer other)
            {
                if (_priority > other._priority) return 1;
                else if (_priority < other._priority) return -1;
                else return 0;
            }
            public T Item => _item;
            public float Priority => _priority;
        }

    }
}