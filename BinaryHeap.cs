using System;

namespace DataStructures
{
    /// <summary>
    /// Basic binary heap, which stores elements in array. 
    /// </summary>
    public class BinaryHeap<T> where T : IComparable<T>
    {
        private const int defaultCapacity = 8;
        /// <summary>
        /// 1 - descending, -1 - ascending
        /// </summary>
        private int _order;
        private int _count;
        private T[] _data;
        public int Count => _count;
        public bool Descending => _order == 1;

        public BinaryHeap(bool desceinding = true)
        {
            _count = 0;
            _data = new T[defaultCapacity];
            _order = desceinding ? 1 : -1;
        }
        public BinaryHeap(int initialCapacity, bool desceinding = true)
        {
            if (initialCapacity < 0) throw new ArgumentException("Capacity can't be lesser than 1");
            _count = 0;
            _data = new T[initialCapacity];
            _order = desceinding ? 1 : -1;
        }
        /// <summary>
        /// Reference to top element. Doesn't perform extraction.
        /// </summary>
        public ref T Top()
        {
            if (_count == 0) throw new Exception("No elements in heap");
            return ref _data[0];
        }
        public void Insert(T element)
        {
            if (element == null) throw new ArgumentNullException("Element can't be null");
            AllocateIfNeeded(_count);
            _data[_count] = element;
            int currentIndex = _count;
            int parentIndex = ParentIndex(currentIndex);

            while (currentIndex != 0 && _data[parentIndex].CompareTo(_data[currentIndex]) == _order)
            {
                T tmp = _data[parentIndex];
                _data[parentIndex] = _data[currentIndex];
                _data[currentIndex] = tmp;
                currentIndex = parentIndex;
                parentIndex = ParentIndex(currentIndex);
            }
            _count++;
        }
        /// <summary>
        /// Extracts and returns top element.
        /// </summary>
        public T Extract()
        {
            if (_count == 0) throw new Exception("No elements in heap");
            T result = _data[0];
            _data[0] = _data[_count - 1];
            _count--;

            int currentIndex = 0;
            bool done = false;
            while (!done)
            {
                int childrenCount = ChildrenCount(currentIndex);
                int leftChildIndex = ChildLeftIndex(currentIndex);
                int rightChildIndex = ChildRightIndex(currentIndex);

                int swapCandidate = -1;
                if (childrenCount == 2)
                {
                    swapCandidate =
                        _data[leftChildIndex].CompareTo(_data[rightChildIndex]) == _order ?
                        rightChildIndex : leftChildIndex;
                }
                else if (childrenCount == 1) swapCandidate = leftChildIndex;

                if (swapCandidate != -1)
                {
                    if (_data[currentIndex].CompareTo(_data[swapCandidate]) == _order)
                    {
                        T tmp = _data[currentIndex];
                        _data[currentIndex] = _data[swapCandidate];
                        _data[swapCandidate] = tmp;
                        currentIndex = swapCandidate;
                    }
                    else done = true;
                }
                else done = true;
            }

            return result;
        }
        private int ParentIndex(int index) => (index - 1) / 2;
        private int ChildLeftIndex(int index) => index * 2 + 1;
        private int ChildRightIndex(int index) => index * 2 + 2;
        private int ChildrenCount(int index) => index switch
        {
            int x when x * 2 + 2 <= _count => 2,
            int x when x * 2 + 1 == _count => 1,
            _ => 0
        };
        private void AllocateIfNeeded(int capacity)
        {
            if (capacity >= _data.Length)
            {
                T[] tmp = _data;
                _data = new T[capacity * 2];
                tmp.CopyTo(_data, 0);
            }
        }
        public override string ToString()
        {
            return $"BinaryHeap: Count = {_count}";
        }
    }
}